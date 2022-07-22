using System;
using System.Collections.Generic;
using Core.Enemy.Loot.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Player.Bag
{
    public class Bag : MonoBehaviour
    {
        [SerializeField] private int _currentCount;
        [SerializeField] private int _maxCount;
        public event Action<int> OnUpdateBag;
        public bool HasCanSpend => _currentCount > 0;
        public bool HasCanAdd => _currentCount + 1 <= _maxCount;

        private void Start()
        {
            OnUpdateBag?.Invoke(_currentCount);
        }

        [Button]
        public void Add()
        {
            _currentCount++;
            OnUpdateBag?.Invoke(_currentCount);
        }

        [Button]
        public void Spend()
        {
            _currentCount--;
            OnUpdateBag?.Invoke(_currentCount);
        }

    }
}