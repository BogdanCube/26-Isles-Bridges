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
                _currentTarget = enemy.transform;
                _currentHealth = enemy.HealthComponent;
                //_displayHealth.ShowBar();
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy.Enemy))
                {
                    _currentTarget = tower.transform;
                    _currentHealth = tower.HealthComponent;
                    //_displayHealth.ShowBar();
                }
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy.Enemy enemy))
            {
                if (_isNullExit)
                {
                    _currentTarget = null;
                }
                //_displayHealth.HideBar();
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy.Enemy))
                {
                    _currentTarget = null;
                    //_displayHealth.HideBar();
                }
            }
        }
    }
}

