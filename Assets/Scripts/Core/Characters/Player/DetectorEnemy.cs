using Core.Character;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Characters.Player
{
    public class DetectorEnemy : DetectorFighting
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy) && other.TryGetComponent(out IHealthComponent ihealthComponent))
            {
                _currentTarget = enemy.transform;
                _currentHealth = ihealthComponent;
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy.Enemy))
                {
                    _currentTarget = tower.transform;
                    _currentHealth = tower.HealthComponent;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy) && other.TryGetComponent(out IHealthComponent ihealthComponent))
            {
                _currentTarget = null;

            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy.Enemy))
                {
                    _currentTarget = null;
                }
            }
        }
    }
}

