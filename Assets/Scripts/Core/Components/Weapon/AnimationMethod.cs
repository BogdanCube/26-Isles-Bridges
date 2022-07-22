using Core.Character;
using Core.Components;
using UnityEngine;

namespace Components.Weapon
{
    public class AnimationMethod : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private DetectorFighting _detectorFighting;

        public void Attack()
        {
            var target = _detectorFighting.CurrentTarget;

            if (target != null)
            {
                _weapon.TakeDamage(target);
            }
        }
    }
}