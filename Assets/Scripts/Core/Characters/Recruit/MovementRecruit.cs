using System;
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
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private DetectorFighting _outsideDetector;
        private Base.Character _owner;
        public override bool IsMove => _navMeshAgent.velocity.sqrMagnitude > 0;

        private void Start()
        {
            _navMeshAgent.speed = _speed;
        }

        public override void Move()
        {
            if (_outsideDetector.IsFight)
            {
                var target = _outsideDetector.CurrentTarget.position;
                _navMeshAgent.SetDestination(target);
                transform.DOLookAt(target, 0.5f);
            }
        }

        public void MoveToTarget(Vector3 target)
        {
            if (_outsideDetector.IsFight == false)
            {
                _navMeshAgent.SetDestination(target);
                transform.DOLookAt(target, 0.5f);
            }
        }

        public void Initialization(Base.Character owner)
        {
            _owner = owner;
        }

        public void StopMove()
        {
            _healthComponent.Death();
        }
    }
}