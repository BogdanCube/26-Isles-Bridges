using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class Bag : ProgressComponent
    {
        [SerializeField] private int _currentCount;
        public event Action<int> OnUpdateBag;
        public bool HasCanSpend => _currentCount > 0;
        public bool HasCanAdd => _currentCount + 1 <= _maxCount;

        private void Start()
        {
            Load();
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