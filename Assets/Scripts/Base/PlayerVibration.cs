using System;
using Base.Level;
using Core.Characters.Player;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Base
{
    public class PlayerVibration : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        [SerializeField] private GamePresent _gamePresent;
        private Player _player;

        #region Enable / Disable
        private void Start()
        {
            _player = _loaderLevel.CurrentPlayer;
            _player.Bag.OnUpdateBag += UpdateBag;
            _player.Weapon.OnTakeDamage += TakeDamage;
            _gamePresent.OnStartGame.AddListener(StartLevel);
            _gamePresent.OnWin.AddListener(Win);
            _gamePresent.OnLose.AddListener(Lose);
        }
        private void OnDisable()
        {
            _player.Bag.OnUpdateBag -= UpdateBag;
            _player.Weapon.OnTakeDamage -= TakeDamage;
            _gamePresent.OnStartGame.RemoveListener(StartLevel);
            _gamePresent.OnWin.RemoveListener(Win);
            _gamePresent.OnLose.RemoveListener(Lose);
        }
        #endregion

        #region Vibration

        private void StartLevel()
        {
            MMVibrationManager.Haptic (HapticTypes.Selection);
        }
        private void Win()
        {
            MMVibrationManager.Haptic (HapticTypes.Success);
        }
        private void Lose()
        {
            MMVibrationManager.Haptic (HapticTypes.Warning);
        }
        private void UpdateBag(int count)
        {
            MMVibrationManager.Haptic (HapticTypes.LightImpact);
        }
        private void TakeDamage()
        {
            MMVibrationManager.Haptic (HapticTypes.SoftImpact);
        }
        #endregion
    }
}