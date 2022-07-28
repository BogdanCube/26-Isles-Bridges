using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._ProgressComponents.Health
{
    public class HealthComponent : ProgressComponent, IHealthComponent
    {        
        [SerializeField] private int _currentCount;
        public event Action<int> OnUpdateHealth;
        public Action OnDeath { get; set; }

        public bool IsDeath
        {
            get => _currentCount <= 0;
            set => throw new NotImplementedException();
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

        private void UpdateCount()
        {
            _currentCount = _maxCount;
            OnUpdateHealth?.Invoke(_currentCount);
        }

        public void Heal(int count)
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
            if (_currentCount <= 0)
            {
                _currentCount = 0;
                OnDeath?.Invoke();
            }
            OnUpdateHealth?.Invoke(_currentCount);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            UpdateCount();
        }

        #region Button Methood
        [Button]
        private void HitByOne()
        {
            Hit(1);
        }
        [Button]
        private void Death()
        {
            _currentCount *= 0;
            Hit(1);
        }
        #endregion

    }
}