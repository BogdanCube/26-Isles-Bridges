using System;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderPlayer : DebugDetector
    {
        private Player.Player _player;
        public bool IsPlayer =>_player && _player.HealthComponent.IsDeath == false;
        public Transform Player => _player.transform;

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