using Core.Characters.Enemy.Finder;
using Core.Components;
using Core.Components._ProgressComponents.Bag;
using Core.Components.DataTowers;
using Core.Environment.Tower._Base;
using NaughtyAttributes;
using NTC.Global.Cache;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Characters.Enemy
{
    public class MovementEnemy : MovementController
    {
        [Header("Setting")]
        [SerializeField] private Transform _currentTarget;
        [HorizontalLine(color: EColor.Red)][SerializeField] private Transform _startPos;

        [Header("Components")]
        [SerializeField] private FinderOutside _finderOutside;
        [SerializeField] private HealthTower _healthTower;
        private Transform _aheadTarget;
        private NavMeshPath _navMeshPath;
        private bool _isAhead;
        public bool IsAhead => _isAhead &&_aheadTarget && transform.DistanceToTarget(_aheadTarget) < _finderOutside.Radius;
        public bool IsAtStart => transform.DistanceToTarget(_startPos) < 1;

        #region Ahead
        private void OnEnable()
        {
            _healthTower.OnHit += SetAhead;
        }

        private void OnDisable()
        {
            _healthTower.OnHit -= SetAhead;
        }
        public void SetAhead(Transform target)
        {
            _aheadTarget = target;
        }
        #endregion
        private void Start()
        {
            _navMeshAgent.speed = _speed;
            _navMeshPath = new NavMeshPath();
        }

       
        public override void Move()
        {
            if (IsStopped) return;
            base.Move();
            
            
            if (_currentTarget)
            {
                _navMeshAgent.SetDestination(_currentTarget.position);
                if (Vector3.Distance(transform.position, _currentTarget.position) < 0.2f ||
                    _currentTarget.gameObject.activeSelf == false)
                {
                    _currentTarget = null;

                }
            }
        }
       
        

        #region SetTarget
        public void UpdatePos()
        {
            _navMeshAgent.SetRandomDestination(5);
        }
        public void SetStartTarget()
        {
            SetTarget(_startPos);
        }
        public void SetTargetAhead()
        {
            SetTarget(_aheadTarget);
        }
        public void SetTarget(Transform target)
        {
            if (target.gameObject.activeSelf)
            {
                _currentTarget = null;
                _currentTarget = target;
                _navMeshAgent.SetDestination(_currentTarget.position);
            }
        }
        public void SetCheckTarget(Transform target)
        {
            _navMeshAgent.CalculatePath(target.position, _navMeshPath);
            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                SetTarget(target);
            }
        }
        

        #endregion
    }
}