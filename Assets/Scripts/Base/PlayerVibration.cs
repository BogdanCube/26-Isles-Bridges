using Base.Level;
using Core.Character.Player;
using Core.Characters.Player;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Base
{
    public class PlayerVibration : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        private Player _player;

        #region Enable / Disable
        private void OnEnable()
        {
            _player = _loaderLevel.CurrentPlayer.GetComponent<Player>();
            _player.Bag.OnUpdateBag += UpdateBag;
            _player.Weapon.OnTakeDamage += TakeDamage;
        }
        private void OnDisable()
        {
            _player.Bag.OnUpdateBag -= UpdateBag;
            _player.Weapon.OnTakeDamage -= TakeDamage;
        }
        #endregion
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