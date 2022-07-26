using System;
using Core.Components._ProgressComponents;
using Core.Environment.Tower.ShopProgressItem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Core.Components.Health
{
    public class HealthComponent : ProgressComponent
    {        
        [ProgressBar("Health", 20, EColor.Red)] 
        [SerializeField] private int _currentCount;
        public event Action<int> OnUpdateHealth;
        public bool IsDeath => _currentCount <= 0;
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

        public void Heal(int healHealth)
        {
            _currentCount += healHealth;
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