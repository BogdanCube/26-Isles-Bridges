using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class Bag : ProgressComponent
    {
        [SerializeField] private int _currentCount;
        public event Action<int> OnUpdateBag;
        public bool HasCanSpend(int count = 1) => _currentCount >= count;
        public bool HasCanAdd => _currentCount + 1 <= _maxCount;
        public int CurrentCount => _currentCount;
        public bool CheckCount(float percentage) => _currentCount > percentage * _maxCount;
        public int MaxCount => _maxCount;
        private void Start()
        {
            if (IsProgress)
            {
                Load();
            }
            OnUpdateBag?.Invoke(_currentCount);
        }
        [Button]
        public void Add(int count = 1)
        {
            _currentCount += count;
            OnUpdateBag?.Invoke(_currentCount);
        }

        [Button]
        public void Spend(int count = 1)
        {
            _currentCount -= count;
            OnUpdateBag?.Invoke(_currentCount);
        }


        public void Reset()
        {
            _currentCount = 0;
            OnUpdateBag?.Invoke(_currentCount);
        }
    }
}