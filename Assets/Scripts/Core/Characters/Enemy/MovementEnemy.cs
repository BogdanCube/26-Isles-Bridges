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
        [SerializeField] private float _durationLooking;
        [SerializeField] private float _timeErase;

        [SerializeField] private Transform _startPos;
        [SerializeField] private int _randomRadius;
        [SerializeField] private Transform _currentTarget;

        [Header("Components")]
        [SerializeField] private Bag _bag;
        [SerializeField] private DataProgressComponent _dataProgress;
        [SerializeField] private EnemyFinderInside _finderInside;
        [SerializeField] private EnemyFinderOutside _finderOutside;
        private NavMeshPath _navMeshPath;
        public override bool IsMove => _navMeshAgent.isStopped == false;

        private void Start()
        {
            _navMeshAgent.speed = _speed;
            _navMeshPath = new NavMeshPath();

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
            if (_finderOutside.Player && _finderOutside.Player.HealthComponent.IsDeath == false)
            {
                SetTarget(_finderOutside.Player.transform);
            }
            /*else if(_finderInside.ShopTower && _dataProgress.CanBuySomething)
            {
                SetTarget(_finderInside.ShopTower.transform);
            }
            else if (_finderOutside.Island)
            
                if(_finderInside.IsFree)
                {
                    SetTarget(_finderInside.Island.transform);
                }
                else if (_finderOutside.NoBuilding)
                {
                    SetTarget(_finderOutside.NoBuilding.transform);
                    if(_bag.CheckCount(1) &&_finderInside.NoBuilding)
                    {
                        //&& _finderOutside.IsTower
                        SetTarget(_finderInside.NoBuilding.transform);
                    }
                }
            }*/
            else
            {
                if (_bag.IsZero ||_bag.CheckCount(0.2f) == false)
                { 
                    SetTarget(_startPos);
                }
                else if(_bag.HasCanAdd && _finderOutside.Item && _bag.CheckCount(0.7f) == false)
                {
                    SetTarget(_finderOutside.Item.transform);
                }
                else if(_bag.CheckCount(0.8f) && _finderOutside.IsTower)
                {
                    SetTarget(_finderOutside.Tower.transform);
                }
                else if(_bag.CheckCount(0.9f) && _finderOutside.IsBrick)
                {
                    SetTarget(_finderOutside.Brick.transform);
                }
                
            }
            /**/
            
            
            /*else if (_bag.HasCanSpend())
            {
                SetTarget(_finderOutside.Tower.transform);
            }#1#*/
            
        }
        private void SetTarget(Transform target)
        {
            _navMeshAgent.CalculatePath(target.position, _navMeshPath);
            if (target.gameObject.active )
            {
                _currentTarget = target;
                _navMeshAgent.SetDestination(_currentTarget.position);
            }
        }
    }
}