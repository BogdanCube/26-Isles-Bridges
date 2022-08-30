using System;
using Core.Environment.Tower;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components._ProgressComponents.Health
{
    public class HealthComponent : ProgressComponent, IHealthComponent
    {        
        [SerializeField] private int _currentCount;
        [SerializeField] private ParticleSystem _particleHit;
        private bool _isOver;
        public event Action<int> OnUpdateHealth;
        public event Action<Transform> OnHit;
        public event Action OnDeath;
        public event Action OnRespawn;
        public event Action OnOver;
        public int CurrentCount => _currentCount;
        public bool IsDeath
        {
            get => _currentCount <= 0;
            set => throw new NotImplementedException();
        }
        
        public bool IsOver
        {
            get => _isOver;
            set
            {
                _isOver = value; 
                OnOver?.Invoke();
                NightPool.Despawn(transform);
            }
        }
       
        public int MaxCount => _maxCount;
        private void Start()
        {
            if (IsProgress)
            {
                Load();
            }
            UpdateCount();
        }
        
        public void Heal(int count = 1)
        {
            _currentCount += count;
            if (_currentCount > _maxCount)
            {
                _currentCount = _maxCount;
            }
            OnUpdateHealth?.Invoke(_currentCount);
        }
        public void Hit(int damage)
        {
            _currentCount -= damage;
            _particleHit.gameObject.SetActive(true);
            if (_currentCount <= 0)
            {
                _currentCount = 0;
                OnDeath?.Invoke();
            }
            OnUpdateHealth?.Invoke(_currentCount);
        }
        
        public void Respawn()
        {
            OnRespawn?.Invoke();
            Heal(_maxCount);
        }
        protected override void UpdateCount()
        {
            _currentCount = _maxCount;
            OnUpdateHealth?.Invoke(_currentCount);
        }
        [Button]
        private void HitByOne()
        {
            Hit(1);
        }
        [Button]
        public void Death()
        {
            _currentCount *= 0;
            Hit(1);
        }

        public void Over()
        {
            IsOver = true;
        }
    }
}