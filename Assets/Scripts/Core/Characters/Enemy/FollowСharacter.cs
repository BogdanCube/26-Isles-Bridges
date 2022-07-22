using Core.Character.Behavior;
using DG.Tweening;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class FollowСharacter : MovementController
    {
        [Space][SerializeField] private protected Transform _target;
        public override bool IsMove => _target != null;
        
        public virtual void Start()
        {
            _navMeshAgent.speed = _speed;
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

