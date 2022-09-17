using System;
using Core.Components.Wallet;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Tutorial
{
    public class TutorialWalletEvent : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private int _count;
        public UnityEvent OnCollect;

        private void OnEnable()
        {
            _wallet.OnUpdateCount += UpdateCount;
        }

        private void OnDisable()
        {
            _wallet.OnUpdateCount -= UpdateCount;
        }

        private void UpdateCount(int count)
        {
            if (_count == count)
            {
                OnCollect?.Invoke();
            }
        }
    }
}