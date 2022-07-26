using Core.Character;
using Core.Components;
using Core.Components.Behavior;
using DG.Tweening;
using UnityEngine;

namespace Components.Weapon
{
    public class AnimationMethod : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] private BehaviourSystem _behaviourSystem;
        public void Attack()
        {
            var target = _detectorFighting.CurrentTarget;

            if (target != null)
            {
                _weapon.TakeDamage(target);
            }
        }

        public void DisableBehaviour()
        {
            _behaviourSystem.enabled = false;
        }
    }
}