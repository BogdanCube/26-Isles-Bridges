using Core.Character;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class DetectorPlayer : DetectorFighting
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player player))
            {
                _currentTarget = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player player))
            {
                _currentTarget = null;
            }
        }
    }
}