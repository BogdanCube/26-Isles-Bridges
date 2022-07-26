using Core.Character.Behavior;
using DG.Tweening;
using UnityEngine;

namespace Core.Characters.Recruit
{
    public class MovementRecruit : MovementController
    {
        [Space][SerializeField] private protected Transform _target;
        [SerializeField] private float _stoppingDistance;
        public override bool IsMove => _target != null && DistanceToTarget > _stoppingDistance;
        private float DistanceToTarget => Vector3.Distance(transform.position, _target.position);

        private void Start()
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.stoppingDistance = _stoppingDistance;
        }
        
        public override void Move()
        {
            _navMeshAgent.SetDestination(_target.position);
            transform.DOLookAt(_target.position, 0.5f);
        }

        public void SetTarget(Transform target)
        {
            if (_target == null)
            {
                _target = target;
            }
        }
    }
}