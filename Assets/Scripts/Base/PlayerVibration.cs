using Base.Level;
using Core.Character.Player;
using Core.Characters.Player;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Base
{
    public class PlayerVibration : MonoBehaviour
    {
        private Player _player;

        public void Load(Player player)
        {
            _player = player;
            _player.Bag.OnUpdateBag += UpdateBag;
            _player.Weapon.OnTakeDamage += TakeDamage;
        }
        private void OnDisable()
        {
            _player.Bag.OnUpdateBag -= UpdateBag;
            _player.Weapon.OnTakeDamage -= TakeDamage;
        }
        private void UpdateBag(int count)
        {
            MMVibrationManager.Haptic (HapticTypes.LightImpact);
        }
        private void TakeDamage()
        {
            MMVibrationManager.Haptic (HapticTypes.SoftImpact);
        }
    }
}