using Core.Character;
using Core.Characters.Base.Behavior;
using Core.Components;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Components.Weapon
{
    public class AnimationMethod : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] private bool _isVibration = false;
        public void Attack()
        {
            var target = _detectorFighting.CurrentTarget;
            var health = _detectorFighting.CurrentHealth;

            if (target != null)
            {
                if (_isVibration)
                {
                    MMVibrationManager.Haptic (HapticTypes.SoftImpact);
                }
                _weapon.TakeDamage(target,health);
            }
        }
    }
}