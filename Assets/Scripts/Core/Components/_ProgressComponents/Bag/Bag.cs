using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Components._ProgressComponents.Bag
{
    public class Bag : ProgressComponent
    {
        [SerializeField] private int _currentCount;
        public event Action<int> OnUpdateBag;
        public bool HasCanSpend(int count = 1) => _currentCount >= count;
        public bool HasCanAdd => _currentCount + 1 <= _maxCount && IsBlockAdd;
        public int CurrentCount => _currentCount;
        public bool CheckCount(float percentage) => _currentCount > percentage * _maxCount;
        public bool IsZero => _currentCount == 0;
        public int MaxCount => _maxCount;
        private bool _isBlockAdd = true;
        public bool IsBlockAdd
        {
            get => _isBlockAdd;
            set
            {
                if (value != _isBlockAdd)
                {
                    _isBlockAdd = value;
                }
            }
        }
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
            if (_currentCount + count < 0)
            {
                _currentCount = 0;
            }
            else
            {
                _currentCount += count;
            }
            UpdateCount();
        }

        [Button]
        public virtual void Spend(int count = 1)
        {
            if (_currentCount - count < 0)
            {
                _currentCount = 0;
            }
            else
            {           
                _currentCount -= count;
            }
            UpdateCount();
        }
        public void Reset()
        {
            _currentCount = 0;
            UpdateCount();
        }

        public IEnumerator MovedCount(Bag bag, float pumpingSpeed,Action callback = null)
        {
            while (true)
            {
                if (bag.HasCanSpend() && HasCanAdd)
                {
                    Add();
                    bag.Spend();
                }
                else
                {
                    callback?.Invoke();
                    break;
                }
                yield return new WaitForSeconds(pumpingSpeed);
            }

        }
        protected override void UpdateCount()
        {
            OnUpdateBag?.Invoke(_currentCount);
        }
    }
}