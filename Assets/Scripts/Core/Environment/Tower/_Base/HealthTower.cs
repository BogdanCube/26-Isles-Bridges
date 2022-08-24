using System;
using Base.Level;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Components.Loot;
using Core.Environment.Tower._Base;
using DG.Tweening;
using NaughtyAttributes;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class HealthTower : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private LootSpawner _lootSpawner;
        [SerializeField] private LoaderTower _loaderTower;
        [SerializeField] private ParticleSystem _particleHit;
        public event Action<Transform> OnHit;
        public event Action OnDeath;
        public event Action OnOver;

        public bool IsDeath => _bag.CurrentCount <= 0;
        
        
        [Button]
        public void Hit(int damage = 1)
        {
            _towerLevel.Hit(damage, Over);
            
            _loaderTower.ResetTower();
            OnHit?.Invoke(_tower.Island.transform);
            _particleHit.gameObject.SetActive(true);
        }
        [Button]
        public void Over()
        {
            OnOver?.Invoke();
            _lootSpawner?.DespawnLoot();
            transform.DOScale(0, 1f).OnComplete(() =>
            {
                _tower.ReturnNoBuilding();
                LoaderLevel.Instance.UpdateBake();
                transform.Deactivate();
            });
        }
    }
}