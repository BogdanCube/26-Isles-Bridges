using System;
using Core.Components._ProgressComponents;
using Core.Components.Wallet;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Serialization;

namespace Core.Environment.Tower.ShopProgressItem
{
    public class ShopProgressComponent : MonoBehaviour
    {
        [SerializeField] private ProgressComponent _progressComponent;
        [SerializeField] private DetectorWallet _detectorWallet;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _progressText;

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _priceText.text = _progressComponent.Price.ToString();
            _progressText.text = _progressComponent.ProgressText;
        }

        public void Buy()
        {
            var wallet = _detectorWallet.Wallet;
            var price = _progressComponent.Price;
            if (wallet.HasCanSpend(price))
            {
                if (_progressComponent.IsMaxLevel == false)
                {
                    _progressComponent.LevelUp();
                    wallet.Spend(price);
                    UpdateText();
                    if (_progressComponent.IsMaxLevel)
                    {
                        _priceText.text = "MAX";
                        _progressText.text = String.Empty;
                    }
                }
            } 
            else
            {
                _priceText.color = Color.red;
                _priceText.DOColor(Color.white, 1f);
            }
            
        }
    } 
}