using System;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Characters.Recruit
{
    public class ToggleNavMeshAgent : MonoBehaviour
    {
        [SerializeField] private MovementRecruit _movementRecruit;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Base.Character character))
            {
                if (character == _movementRecruit.Owner)
                {
                    _movementRecruit.ToogleMove(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Base.Character character))
            {
                if (character == _movementRecruit.Owner)
                {
                    _movementRecruit.ToogleMove(false);
                }
            }
        }
    }
}