using System.Collections;
using System.Collections.Generic;
using Core.Character.Behavior;
using Core.Characters.Enemy.Finder;
using Core.Components;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.OwnerRecruit;
using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Core.Characters.Enemy
{
    public class MovementEnemy : MovementController
    {
        [SerializeField] private Transform _startPos;
        [SerializeField] private int _randomRadius;
        [SerializeField] private Transform _currentTarget;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] private Bag _bag;
        [SerializeField] private DetachmentRecruit _detachmentRecruit;
        [SerializeField] private EnemyFinderInside _finderInside;
        [SerializeField] private EnemyFinderOutside _finderOutside;
        public override bool IsMove => _navMeshAgent.isStopped == false;

        private void Start()
        {
            _navMeshAgent.speed = _speed;
            StartCoroutine(EraseTarget(3));
        }

        public override void Move()
        {
            if (_finderOutside.Player)
            {
                SetTarget(_finderOutside.Player.transform);
            }
            else if(_finderInside.IsFree)
            {
                SetTarget(_finderInside.Island.transform);
            }
            else if(_bag.CheckCount(0.4f) && _finderOutside.IsBrick)
            {
                SetTarget(_finderOutside.Brick.transform);
            }
            else if(_bag.CheckCount(0.5f) &&_finderInside.NoBuilding)
            {
                SetTarget(_finderInside.NoBuilding.transform);
            }
            else if (_bag.CheckCount(0.8f) && _finderOutside.IsTower)
            {
                SetTarget(_finderOutside.Tower.transform);
            }
            
            else if(_bag.HasCanAdd && _finderOutside.Item)
            {
                SetTarget(_finderOutside.Item.transform);
            }
            else if (_bag.HasCanSpend())
            {
                SetTarget(_finderOutside.Tower.transform);
            }
            else
            {
                SetTarget(_startPos);
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
            if (target.gameObject.activeSelf)
            {
                _currentTarget = target;
                _navMeshAgent.SetDestination(_currentTarget.position);
            }
        }
    }
}