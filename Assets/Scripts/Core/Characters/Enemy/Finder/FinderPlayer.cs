using System;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderPlayer : DebugDetector
    {
        private Player.Player _player;
        private Player.Player _tempPlayer;
        public bool IsPlayer =>_player && _player.HealthComponent.IsDeath == false;
        public Transform Player => _player.transform;
        public Transform FindPlayer => _tempPlayer.transform;
        public HealthComponent PlayerHealth => _tempPlayer.HealthComponent as HealthComponent;
        private void Awake()
        {
            _tempPlayer = FindObjectOfType<Player.Player>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player) && other.TryGetComponent(out HealthComponent healthComponent))
            {
                if (healthComponent.IsDeath == false)
                {
                    _player = player;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        { 
            if (other.TryGetComponent(out Player.Player player) && other.TryGetComponent(out HealthComponent healthComponent))
            {
                _player = null;
            }
        }
    }
}