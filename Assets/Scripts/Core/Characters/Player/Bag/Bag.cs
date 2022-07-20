using System;
using System.Collections.Generic;
using Core.Enemy.Loot.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Player.Bag
{
    public class Bag : MonoBehaviour
    {
        [SerializeField] private int _maxCount;
        [SerializeField] private int _currentCount;

        public event Action<int> OnUpdateBag;

        private void Start()
        {
            OnUpdateBag?.Invoke(_currentCount);
        }

        [Button]
        public void Add()
        {
            if (_currentCount + 1 <= _maxCount)
            {
                _currentCount++;
                OnUpdateBag?.Invoke(_currentCount);
            }
        }

        [Button]
        public void Remove()
        {
            if (_currentCount > 0)
            {
                _currentCount--;
                OnUpdateBag?.Invoke(_currentCount);
            }
        }
        
    }
}