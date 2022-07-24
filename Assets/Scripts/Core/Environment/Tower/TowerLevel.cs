using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components._ProgressComponents.Bag;
using Core.Components.Wallet;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class TowerLevel : MonoBehaviour
    {
        [Range(0,5)] [SerializeField] private int _level;
        [SerializeField] private float _pumpingSpeed;
        [SerializeField] private Wallet _shopWallet;
        [SerializeField] private LoaderTower _loaderTower;
        
        public Action<int,int> OnUpdateDisplayed;
        public Action OnMaxUpgrade;
        public bool IsMaxLevel => _level + 1 >= _loaderTower.MaxLevel;
        
        private void Start()
        {
            UpdateDisplay();
        }

        [Button]
        private void LevelUp()
        {
            _level++;
            //particle
            _loaderTower.Load(_level);
        }
        public IEnumerator ReplenishmentBlock(Wallet wallet)
        {
            while (IsMaxLevel == false)
            {
                yield return new WaitForSeconds(_pumpingSpeed);
                if (wallet.HasCanSpend())
                {
                    wallet.Spend();
                    _shopWallet.Add();
                    if (_shopWallet.CurrentCount >= _loaderTower.PriceNextLevel(_level))
                    {
                        LevelUp();
                        _shopWallet.Reset();
                    }
                    UpdateDisplay();
                }
            }
        }

        private void UpdateDisplay()
        {
            OnUpdateDisplayed.Invoke(_shopWallet.CurrentCount, _loaderTower.PriceNextLevel(_level));
            if (IsMaxLevel)
            {
                OnMaxUpgrade.Invoke();
            }
        }
    }
}