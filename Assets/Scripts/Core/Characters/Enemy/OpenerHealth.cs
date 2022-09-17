using System;
using Core.Characters.Enemy.Finder;
using Core.Components._ProgressComponents.Health;
using Core.Components.DataTowers;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class OpenerHealth : MonoBehaviour
    {
        [SerializeField] private DataProgressComponent _dataProgress;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private FinderPlayer _finderPlayer;
        private bool _isOpened;
        private void Start()
        {
            _finderPlayer.PlayerHealth.OnLevelUp += AddHealth;
        }

        private void OnDisable()
        {
            _finderPlayer.PlayerHealth.OnLevelUp -= AddHealth;
        }

        private void AddHealth()
        {
            if (_isOpened == false)
            {
                _dataProgress.AddComponent(_healthComponent);
                _isOpened = true;
            }
        }
    }
}