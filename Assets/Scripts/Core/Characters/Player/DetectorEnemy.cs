using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using Core.Environment.Tower._Base;
using UnityEngine;

namespace Core.Characters.Player
{
    public class DetectorEnemy : DetectorFighting
    {
        private void OnTriggerStay(Collider other)
        {
            if(IsLive() == false) return;
            
            if (other.TryGetComponent(out Enemy.Enemy enemy))
            {
                _currentTarget = enemy.transform;
                _currentHealth = enemy.HealthComponent;
            }
            else 
            {
                if (other.TryGetComponent(out Tower tower) && tower.Owner is Enemy.Enemy)
                {
                    _currentTarget = tower.transform;
                    _currentHealth = tower.HealthComponent;
                }
            }
            
            bool IsLive()
            {
                return other.TryGetComponent(out IHealthComponent ihealthComponent) &&
                       ihealthComponent.IsDeath == false;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy) && other.TryGetComponent(out IHealthComponent ihealthComponent))
            {
                _currentTarget = null;

            }
            else if (other.TryGetComponent(out Tower tower) && tower.Owner is Enemy.Enemy)
            {
                _currentTarget = null;
            }
        }
    }
}

