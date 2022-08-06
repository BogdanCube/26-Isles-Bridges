using System;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Components.Loot;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class HealthTower : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private LootSpawner _lootSpawner;
        [SerializeField] private ParticleSystem _particleHit;

        public Action OnDeath { get; set; }

        public bool IsDeath
        {
            get => _bag.CurrentCount <= 0;
            set => throw new System.NotImplementedException();
        }

        [Button]
        public void Hit(int damage = 1)
        {
            _towerLevel.Hit(damage);
            _particleHit.gameObject.SetActive(true);
            if (IsDeath)
            {
                _tower.ReturnNoBuilding();
                _lootSpawner.DespawnLoot();
                _towerLevel.DestroyTower(OnDeath);
            }
        }
    }
}