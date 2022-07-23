using System;
using UnityEngine;

namespace Core.Components.Wallet
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int _countCoin;
        public event Action<int> OnUpdateCount;

        public bool HasSpend(int count) => _countCoin > count;

        private void Start()
        {
            OnUpdateCount?.Invoke(_countCoin);
        }

        public void Add(int count = 1)
        {
            if (count > 0)
            {
                _countCoin += count;
                OnUpdateCount?.Invoke(_countCoin);
            }
        }

        public void Spend(int count)
        {
            _countCoin -= count;
            OnUpdateCount?.Invoke(_countCoin);
        }
    }
}