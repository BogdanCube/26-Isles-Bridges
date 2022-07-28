using System.Collections;
using System.Collections.Generic;
using Core.Character.Behavior;
using Core.Characters.Enemy.Finder;
using Core.Components;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Core.Characters.Enemy
{
    public class MovementEnemy : MovementController
    {
        [SerializeField] private int _randomRadius;
        [SerializeField] private Transform _currentTarget;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] private Bag _bag;
        [SerializeField] private EnemyFinder _enemyFinder;
        [SerializeField] private FinderIsland _finderIsland;
        public override bool IsMove => _navMeshAgent.isStopped == false;

        private void Start()
        {
            _navMeshAgent.speed = _speed;
            StartCoroutine(EraseTarget(5));
        }

        public override void Move()
        {
            if (_enemyFinder.Player)
            {
                SetTarget(_enemyFinder.Player.transform);
            }
            else if(_finderIsland.IsFree)
            {
                SetTarget(_finderIsland.Island.transform);
            }
            else if(_bag.CheckCount && _enemyFinder.Brick.IsSet == false)
            {
                SetTarget(_enemyFinder.Brick.transform);
            }
            else if(_bag.HasCanAdd && _enemyFinder.IsItem)
            {
                SetTarget(_enemyFinder.Item.transform);
            }
            
        }

        private IEnumerator EraseTarget(int time)
        {
            while (true)
            {
                _currentTarget = null;
                _navMeshAgent.SetDestination(_navMeshAgent.RandomPosition(_randomRadius));
                yield return new WaitForSeconds(time);
            }
        }

        private void SetTarget(Transform target)
        {
            _currentTarget = target;
            _navMeshAgent.SetDestination(_currentTarget.position);
        }
    }
}