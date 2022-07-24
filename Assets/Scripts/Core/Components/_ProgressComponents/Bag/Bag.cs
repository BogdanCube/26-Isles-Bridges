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
        public int CurrentCount => _currentCount;

        private void Start()
        {
            if (IsProgress)
            {
                Load();
            }
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

        public void Reset()
        {
            _currentCount = 0;
            OnUpdateBag?.Invoke(_currentCount);
        }
    }
}