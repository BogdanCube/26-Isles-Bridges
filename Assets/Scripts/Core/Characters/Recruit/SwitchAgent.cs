using System;
using Core.Characters._Base;
using Core.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Characters.Recruit
{
    public class SwitchAgent : MonoBehaviour
    {
        [SerializeField] private DetectorFighting _detectorOutside;
        [SerializeField] private MovementRecruit _movementRecruit;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private Character _owner;
        private void Start()
        {
            _owner = _movementRecruit.Owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_detectorOutside.CurrentTarget) return;
            
            if (other.TryGetComponent(out Character character) && character == _owner)
            {
                _movementRecruit.IsStopped = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_detectorOutside.CurrentTarget) return;
            
            if (other.TryGetComponent(out Character character) && character == _owner)
            {
                _movementRecruit.IsStopped = false;
            }
        }
    }
}