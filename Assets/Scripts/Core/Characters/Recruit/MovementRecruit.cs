using Core.Characters._Base;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Components._ProgressComponents.OwnerRecruit;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Recruit
{
    public class MovementRecruit : MovementController
    {
        [SerializeField] private HealthComponent _healthComponent;
        private DetachmentRecruit _detachmentRecruit;
        private Character _owner;
        private Transform _currentTarget;
        private Vector3 _currentPosition;
        private float _distanceTeleport = 5;
        public override bool IsMove => transform.DistanceToTarget(_currentPosition) > 0.75f && IsStopped == false;
        public Character Owner => _owner;
        
        #region Init / Deinit
        public void Init(Character owner, DetachmentRecruit detachmentRecruit)
        {
            _owner = owner;
            _detachmentRecruit = detachmentRecruit;
            _healthComponent.OnDeath += Deinit;
        }

        private void Deinit()
        {
            _detachmentRecruit.Remove(this);
            _healthComponent.OnDeath -= Deinit;
            _healthComponent.Death();
        }
        

        #endregion
        private void Start()
        {
            _navMeshAgent.speed = _speed;
        }
        
        public void MoveToTarget()
        {
            IsStopped = false;
            _navMeshAgent.SetDestination(_currentTarget.position);
        }

        public void MoveToPosition()
        {
            if (IsStopped) return;
            
            if (transform.DistanceToTarget(_currentPosition) < _distanceTeleport)
            {
                _navMeshAgent.SetDestination(_currentPosition);
            }
            else
            {
                SetStartPos(_currentPosition);
            }
        }
        
        public void SetTarget(Transform target)
        {
            _currentTarget = target;
        }
        public void SetPosition(Vector3 position)
        {
            _currentPosition = position;
        }
    }
}