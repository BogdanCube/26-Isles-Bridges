using UnityEngine;

namespace Core.Components.Weapon
{
    public class AnimationMethod : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private DetectorFighting _detectorFighting;
        public void Attack()
        {
            var target = _detectorFighting.CurrentTarget;
            var health = _detectorFighting.CurrentHealth;

            if (target != null)
            {
                _weapon.TakeDamage(target,health);
            }
        }
    }
}