using Core.Character;
using Core.Components;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Characters.Player
{
    public class DetectorEnemy : DetectorFighting
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy))
            {
                _currentTarget = enemy.HealthComponent;
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy.Enemy))
                {
                    _currentTarget = tower.HealthComponent;
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy))
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

