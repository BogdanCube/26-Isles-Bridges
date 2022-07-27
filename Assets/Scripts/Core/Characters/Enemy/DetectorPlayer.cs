using Core.Character;
using Core.Components;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class DetectorPlayer : DetectorFighting
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player player))
            {
                _currentTarget = player.HealthComponent;
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Character.Player.Player))
                {
                    _currentTarget = tower.HealthComponent;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player player))
            {
                _currentTarget = null;
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Character.Player.Player))
                {
                    _currentTarget = null;
                }
            }
        }
    }
}