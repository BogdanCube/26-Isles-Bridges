using System;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Components.Wallet
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int _countCoin;
        [SerializeField] private HealthComponent _healthComponent;
        private float _coefficientLoss = 0.7f;
        public event Action<int> OnUpdateCount;
        public int CurrentCount => _countCoin;
        public bool HasCanSpend(int count = 1) => _countCoin >= count;

        #region Enable / Disable

        private void OnEnable()
        {
            _healthComponent.OnDeath += DeathSpend;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= DeathSpend;
        }

        private void DeathSpend()
        {
            var coin = _countCoin - _countCoin * _coefficientLoss;
            Reset();
            Add((int)coin);
        }

        #endregion
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

        public void Spend(int count = 1)
        {
            _countCoin -= count;
            OnUpdateCount?.Invoke(_countCoin);
        }

        public void Reset()
        {
            _countCoin = 0;
            OnUpdateCount?.Invoke(_countCoin);
        }
    }
}