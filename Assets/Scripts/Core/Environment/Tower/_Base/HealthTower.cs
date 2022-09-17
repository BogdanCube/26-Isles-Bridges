using System;
using Base.Level;
using Core.Components._ProgressComponents.Health;
using Core.Components.Loot;
using DG.Tweening;
using NaughtyAttributes;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Environment.Tower._Base
{
    public class HealthTower : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private LootSpawner _lootSpawner;
        [SerializeField] private ParticleSystem _particleHit;
        private int _maxCount;
        [SerializeField] private int _currentCount;
        public event Action<Transform> OnHit;
        public event Action OnDeath;
        public event Action<int, int> OnUpdateHealth; 
        public event Action OnOver;

        public bool IsDeath => _currentCount <= 0;


        public void Load(int count)
        {
            _maxCount = count;
            _currentCount = _maxCount;
            UpdateCount();
        }
        [Button]
        public void Hit(int damage = 1)
        {
            _currentCount -= damage;
            OnHit?.Invoke(_tower.Island.transform);
            UpdateCount();
            _particleHit.gameObject.SetActive(true);
            
            if (IsDeath)
            {
                _towerLevel.DestroyTower(Over);
            }
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

        private void UpdateCount()
        {
            if (_currentCount < 0)
            {
                _currentCount = 0;
            }
            OnUpdateHealth?.Invoke(_currentCount,_maxCount);

        }
    }
}