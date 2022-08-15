using System;
using Base.Level;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Components.Loot;
using Core.Environment.Tower._Base;
using DG.Tweening;
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
        [SerializeField] private LoaderTower _loaderTower;
        [SerializeField] private ParticleSystem _particleHit;
        private Action _onHit;
        public Action OnHit { get; set; }
        public Action OnDeath { get; set; }
        public Action OnOver { get; set; }
        public bool IsDeath => _bag.CurrentCount <= 0;
        
        private void OnDisable()
        {
            _tower.Owner.HealthComponent.OnOver -= Over;
        }
        
        [Button]
        public void Hit(int damage = 1)
        {
            _towerLevel.Hit(damage);
            _loaderTower.ResetTower();
            OnHit?.Invoke();
            _particleHit.gameObject.SetActive(true);
            if (IsDeath)
            {
                _towerLevel.DestroyTower(Over);
            }
            
        }
        [Button]
        public void Over()
        {
            _lootSpawner.DespawnLoot();
            transform.DOScale(0, 1f).OnComplete(() =>
            {
                OnDeath.Invoke();
                _tower.ReturnNoBuilding();
                Destroy(gameObject);
                LoaderLevel.Instance.UpdateBake();
            });
        }
    }
}