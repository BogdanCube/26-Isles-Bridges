using System;
using Core.Components._ProgressComponents;
using Core.Components._ProgressComponents.Bag;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Core.Environment.Tower.ShopProgressItem
{
    public class ShopProgressComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        private ProgressComponent _currentComponent;
        private Wallet _wallet;
        private int _price;
        public int Price => _price;
        public void Load(ProgressComponent component, Wallet wallet)
        {
            transform.localScale = Vector3.one;
            _currentComponent = component;
            _wallet = wallet;
            _price = component.Price;
            
            _image.sprite = _currentComponent.Icon;
            _priceText.text = _currentComponent.IsMaxLevel ? "MAX" :_price.ToString();
            _progressText.text = _currentComponent.IsMaxLevel ? String.Empty :_currentComponent.ProgressText;
            _button.onClick.AddListener(Buy);
        }
        
        public void Buy()
        {
            if (_wallet.HasCanSpend(_price))
            {
                BuyAhead();
            } 
            else
            {
                _priceText.color = Color.red;
                _priceText.DOColor(Color.white, 1f);
            }
        }

        public void BuyAhead()
        {
            if (_currentComponent.IsMaxLevel == false)
            {
                _currentComponent.LevelUp();
                _wallet.Spend(_price);
                Load(_currentComponent,_wallet);
            }
        }
    } 
}