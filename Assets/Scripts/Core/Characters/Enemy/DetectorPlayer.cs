using Core.Character;
using Core.Components;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class DetectorPlayer : DetectorFighting
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player player))
            {
                _currentTarget = player.transform;
                _currentHealth = player.HealthComponent;
                _displayHealth.ShowBar();
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Character.Player.Player))
                {
                    _currentTarget = tower.transform;
                    _currentHealth = tower.HealthComponent;
                    _displayHealth.ShowBar();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out  Character.Player.Player player))
            {
                if (_isNullExit)
                {
                    _currentTarget = null;
                    _displayHealth.HideBar();
                }
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Character.Player.Player))
                {
                    _currentTarget = null;
                    _displayHealth.HideBar();
                }
            }
        }
    }
}