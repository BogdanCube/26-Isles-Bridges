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
        [SerializeField] private float _durationLooking;
        [SerializeField] private int _randomRadius; 

        [Header("Components")]
        [SerializeField] private Bag _bag;
        [SerializeField] private DataProgressComponent _dataProgress;
        [SerializeField] private DataTowers _dataTowers;
        [SerializeField] private EnemyFinderInside _finderInside;
        [SerializeField] private EnemyFinderOutside _finderOutside;
        public override bool IsMove => _navMeshAgent.isStopped == false;

        private void Start()
        {
            _navMeshAgent.speed = _speed;

            //StartCoroutine(FindTarget(_timeErase));

        }
        public override void Move()
        {
            FindTarget();
            if (_currentTarget && Vector3.Distance(transform.position,_currentTarget.position) > 1)
            {
                transform.SlowLookY(_currentTarget,_durationLooking);
            }
            else
            {
               _currentTarget = null;
               _navMeshAgent.SetRandomDestination(_randomRadius);
            }
        }
        private void FindTarget()
        {
            if (_finderOutside.Target)
            {
                SetTarget(_finderOutside.Target);
            }
            
            else if (_bag.IsZero || _bag.CheckCount(0.5f) == false)
            {
                if (_finderOutside.BlockItem == false)
                {
                    SetTarget(_startPos);
                }
                else if(_bag.HasCanAdd && _finderOutside.BlockItem && _bag.CheckCount(0.8f) == false)
                {
                    SetTarget(_finderOutside.BlockItem);
                }
            }
            else if (_finderOutside.Item)
            {
                SetTarget(_finderOutside.Item);
            }
            else if(_dataProgress.CanBuySomething &&_finderInside.ShopTower)
            {
                SetTarget(_finderInside.ShopTower);
            }
            else
            {
                if (_dataTowers.CanBuySomething() &&_finderOutside.NoBuilding)
                {
                    SetTarget(_finderOutside.NoBuilding);
                }
                if(_bag.CheckCount(0.95f))
                {
                    if (_finderOutside.Tower)
                    {
                        SetTarget(_finderOutside.Tower);
                    }
                    else if(_finderOutside.Brick)
                    {
                        SetTarget(_finderOutside.Brick);
                    }
                }
            }
        }
        private void SetTarget(Transform target)
        {
           
            if (target.gameObject.active)
            {
                _currentTarget = target;
                _navMeshAgent.SetDestination(_currentTarget.position);
            }
        }
    }
}