using Core.Character;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class DetectorPlayer : DetectorFighting
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player) && other.TryGetComponent(out IHealthComponent ihealthComponent))
            {
                _currentTarget = player.transform;
                _currentHealth = ihealthComponent;
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Player.Player))
                {
                    _currentTarget = tower.transform;
                    _currentHealth = tower.HealthComponent;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player) && other.TryGetComponent(out IHealthComponent ihealthComponent))
            {
                _currentTarget = null;
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Player.Player))
                {
                    _currentTarget = null;
                }
            }
        }
    }
}