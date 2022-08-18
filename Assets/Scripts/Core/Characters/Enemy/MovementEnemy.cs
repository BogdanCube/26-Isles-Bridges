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

        [Header("Components")]
        [SerializeField] private Bag _bag;
        [SerializeField] private DataProgressComponent _dataProgress;
        [SerializeField] private DataTowers _dataTowers;
        [SerializeField] private FinderInside _finderInside;
        [SerializeField] private FinderOutside _finderOutside;
        [SerializeField] private FinderPlayer _finderPlayer;
        [SerializeField] private HealthTower _healthTower;
        private NavMeshPath _navMeshPath;
        private bool _isAhead;
        //[ShowNativeProperty] public bool IsStopped => _navMeshAgent.isStopped;
        private bool IsTarget => _finderOutside.IsTarget  || _finderPlayer.IsPlayer;
        public override bool IsMove => _navMeshAgent.velocity.sqrMagnitude > 0;

        #region Enable / Disable
        private void OnEnable()
        {
            _healthTower.OnHit += AheadMove;
        }

        private void OnDisable()
        {
            _healthTower.OnHit -= AheadMove;
        }
        #endregion
        private void Start()
        {
            _navMeshAgent.speed = _speed;
            _navMeshPath = new NavMeshPath();
        }
        public override void Move()
        {
            if (_navMeshAgent.isStopped) return;

            base.Move();
            
            if (_currentTarget && _currentTarget.gameObject.activeSelf && Vector3.Distance(transform.position, _currentTarget.position) > 1)
            {
                //_model.SlowLookY(_currentTarget,1);
            }
            else
            {
                _currentTarget = null;
                _navMeshAgent.SetRandomDestination(25);
            }

            if (_isAhead &&_currentTarget  && transform.DistanceToTarget(_currentTarget) < _finderOutside.Radius)
            {
                _isAhead = false;
                SpeedDeboost();
            }
        }
        private void LateUpdate()
        {
            if (_isAhead) return;
            
            if (IsTarget)
            {
                FindTarget();
            }
            else
            {
                FindFarm();
            }

        }
        public void AheadMove(Transform target)
        {
            SetTarget(target);
            _isAhead = true;
            SpeedBoost();
        }
        private void FindTarget()
        {
            if (_finderOutside.PlayerTower)
            {
                SetTarget(_finderOutside.PlayerTower);
            }
            if (_finderPlayer.IsPlayer)
            {
                SetCheckTarget(_finderPlayer.Player);
            }
        }
        private void FindFarm()
        {
            if (IsTarget) return;
            if (_dataProgress.CanBuySomething && _finderInside.ShopTower)
            {
                SetTarget(_finderInside.ShopTower);
            }
            if (_bag.IsZero || _bag.CheckCount(0.5f) == false)
            {
                if (_finderOutside.BlockItem == false)
                {
                    SetTarget(_startPos);
                }
                if (_bag.HasCanAdd && _finderOutside.BlockItem && _bag.CheckCount(0.9f) == false)
                {
                    SetCheckTarget(_finderOutside.BlockItem);
                }
            }
            else
            {
                if (_finderOutside.IsTower && _bag.CheckCount(0.9f))
                {
                    SetTarget(_finderOutside.Tower);
                }
                if (_finderOutside.IsBrick && _bag.CheckCount(0.7f))
                {
                    SetTarget(_finderOutside.Brick);
                }
                if (_finderOutside.NoBuilding && _dataTowers.CanBuySomething)
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