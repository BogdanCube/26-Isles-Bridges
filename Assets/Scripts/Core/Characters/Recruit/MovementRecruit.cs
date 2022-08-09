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
        private Base.Character _owner;
        private void Start()
        {
            _navMeshAgent.speed = _speed;
        }

        private void MoveToTarget(Transform target)
        {
            print("а");
            _navMeshAgent.SetDestination(target.position);
            transform.SlowLookY(target);
        }

        public void Initialization(Base.Character owner)
        {
            _owner = owner;
            _owner.MovementController.OnChangePosition += MoveToTarget;
        }

        public void StopMove()
        {
            _owner.MovementController.OnChangePosition -= MoveToTarget;
            _healthComponent.Death();
        }
    }
}