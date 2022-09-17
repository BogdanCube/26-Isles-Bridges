using UnityEngine;

namespace Core.Components.Weapon
{
    public class AnimationMethod : MonoBehaviour
    {
        [SerializeField] [InterfaceType(typeof(IWeapon))] private Object _weapon;
        [SerializeField] private DetectorFighting _detectorFighting;
        private IWeapon Weapon => (IWeapon)_weapon;
        
        public void Attack()
        {
            var target = _detectorFighting.CurrentTarget;
            var health = _detectorFighting.CurrentHealth;

            if (target != null)
            {
                Weapon.TakeDamage(target,health);
            }
        }
    }
}