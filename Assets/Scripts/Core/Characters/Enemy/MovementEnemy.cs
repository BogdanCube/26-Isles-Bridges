using System;
using System.Collections;
using System.Collections.Generic;
using Core.Character.Behavior;
using Core.Characters.Enemy.Finder;
using Core.Components;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.OwnerRecruit;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using Core.Environment.Tower;
using DG.Tweening;
using NaughtyAttributes;
using Rhodos.Toolkit.Extensions;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Core.Characters.Enemy
{
    public class MovementEnemy : MovementController
    {
        [Header("Setting")]
        [SerializeField] private Transform _currentTarget;
        [HorizontalLine(color: EColor.Red)][SerializeField] private Transform _startPos;
        [SerializeField] private float _randomRadius;
        [SerializeField] private float _durationLooking;

        [Header("Components")]
        [SerializeField] private Bag _bag;
        [SerializeField] private DataProgressComponent _dataProgress;
        [SerializeField] private FinderInside _finderInside;
        [SerializeField] private FinderOutside _finderOutside;
        [SerializeField] private HealthTower _healthTower;
        private NavMeshPath _navMeshPath;
        private bool IsTarget => _finderOutside.IsTarget || _dataProgress.CanBuySomething;
        public override bool IsMove => transform.position != _startPos.position;

        #region Enable / Disable
        private void OnEnable()
        {
            _healthTower.OnHit += TeleportToSpawn;
        }

        private void OnDisable()
        {
            _healthTower.OnHit -= TeleportToSpawn;
        }

        private void TeleportToSpawn()
        {
            if (transform.DistanceToTarget(_healthTower) > _finderOutside.Radius)
            {
                SetStartPos(_startPos.position);
            }
        }

        #endregion
        private void Start()
        {
            _navMeshAgent.speed = _speed;
            _navMeshPath = new NavMeshPath();

        }
        public override void Move()
        {
            base.Move();

            if (_currentTarget && _currentTarget.gameObject.activeSelf && Vector3.Distance(transform.position,_currentTarget.position) < 1)
            {
                transform.SlowLookY(_currentTarget,_durationLooking);
            }
            else
            {
                _currentTarget = null;
                _navMeshAgent.SetRandomDestination(_randomRadius);
            }
        }
        private void LateUpdate()
        {
            if (IsTarget)
            {
                FindTarget();
            }
            else
            {
                FindFarm();
            }
        }

        private void FindTarget()
        {
            if (_finderOutside.PlayerTower)
            {
                SetTarget(_finderOutside.PlayerTower);
            }
            if (_finderOutside.IsPlayer)
            {
                SetCheckTarget(_finderOutside.Player);
            }
            if (_dataProgress.CanBuySomething && _finderInside.ShopTower)
            {
                SetTarget(_finderInside.ShopTower);
            }
        }
        private void FindFarm()
        {
            if (IsTarget) 
                return;
            
            if (_bag.IsZero ||_bag.CheckCount(0.5f) == false)
            {
                if (_finderOutside.BlockItem == false)
                {
                    SetTarget(_startPos);
                }
                else if (_bag.HasCanAdd && _finderOutside.BlockItem && _bag.CheckCount(0.9f) == false)
                {
                    SetCheckTarget(_finderOutside.BlockItem);
                }
            }
            else
            {
                if (_finderOutside.IsTower)
                {
                    SetTarget(_finderOutside.Tower);
                }
                else if (_finderOutside.IsBrick)
                {
                    SetTarget(_finderOutside.Brick);
                }
                if (_finderOutside.NoBuilding)
                {
                    SetCheckTarget(_finderOutside.NoBuilding);
                }
                if (_finderOutside.Item)
                {
                    SetCheckTarget(_finderOutside.Item);
                }
            }
        }
        
        private void SetTarget(Transform target)
        {
            if (target.gameObject.activeSelf)
            {
                _currentTarget = target;
                _navMeshAgent.SetDestination(_currentTarget.position);
            }
        }

        private void SetCheckTarget(Transform target)
        {
            _navMeshAgent.CalculatePath(target.position, _navMeshPath);
            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                SetTarget(target);
            }
        }
    }
}