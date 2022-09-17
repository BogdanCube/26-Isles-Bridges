using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using Core.Environment.Tower._Base;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class DetectorPlayer : DetectorFighting
    {
        private void OnTriggerStay(Collider other)
        {
            if(IsLive() == false) return;

            if (other.TryGetComponent(out Player.Player player))
            {
                _currentTarget = player.transform;
                _currentHealth = player.HealthComponent;
            }
            else
            {
                if (other.TryGetComponent(out Tower tower) && tower.Owner is Player.Player)
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
            if (other.TryGetComponent(out Player.Player player) && other.TryGetComponent(out IHealthComponent ihealthComponent))
            {
                _currentTarget = null;
            }
            else if (other.TryGetComponent(out Tower tower) && tower.Owner is Player.Player)
            {
                _currentTarget = null;
            }
        }
    }
}