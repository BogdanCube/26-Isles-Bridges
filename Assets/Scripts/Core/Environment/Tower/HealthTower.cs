using System;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class HealthTower : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private TowerLevel _towerLevel;

        public Action OnDeath { get; set; }

        public bool IsDeath
        {
            get => _bag.CurrentCount <= 0;
            set => throw new System.NotImplementedException();
        }
        
        [Button]
        public void Hit(int damage = 1)
        {
            _towerLevel.Hit();
            if (IsDeath)
            {
                OnDeath?.Invoke();
                _towerLevel.DestroyTower();
            }
        }
    }
}