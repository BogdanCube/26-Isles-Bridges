using Core.Character.Behavior;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using NaughtyAttributes;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Recruit
{
    public class MovementRecruit : MovementController
    {
        [SerializeField] private DetectorFighting _outsideDetector;
        [SerializeField] private HealthComponent _healthComponent;
        //[SerializeField] private float _stoppingDistance;
        private Base.Character _owner;
        private Transform CurrentTarget => _outsideDetector.IsFight ? _outsideDetector.CurrentTarget : _owner.transform;
        public override bool IsMove => _navMeshAgent.isStopped == false && (_owner || _outsideDetector.CurrentTarget);
        public Base.Character Owner => _owner;
        public HealthComponent HealthComponent => _healthComponent;
        private void Start()
        {
            _navMeshAgent.speed = _speed;
            //_navMeshAgent.stoppingDistance = _stoppingDistance;
        }
        
        public override void Move()
        {
            _navMeshAgent.SetDestination(CurrentTarget.position);
            transform.SlowLookY(CurrentTarget);
        }

        public void ToogleMove(bool state)
        {
            _navMeshAgent.isStopped = _outsideDetector.CurrentTarget == false && state;
        }

        public void SetOwner(Base.Character owner)
        {
            if (_owner == false)
            {
                _owner = owner;
            }
        }
    }
}