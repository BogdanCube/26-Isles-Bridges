using System;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using NTC.Global.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Environment.Tower.ShopDataTower
{
    public class ShopDataTower : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _button;
        private Components.DataTowers.TemplateTower _currentTemplate;
        private NoBuilding.NoBuilding _noBuilding;
        private BagCharacter _bag;
        private int _price;
        private bool _isBuy = false;

        public int Price => _price;
        public void Load(Components.DataTowers.TemplateTower templateTower, NoBuilding.NoBuilding noBuilding, BagCharacter bag)
        {
            transform.localScale = Vector3.one;
            _currentTemplate = templateTower;
            _noBuilding = noBuilding;
            _bag = bag;
            _price = _currentTemplate.Price;
            
            _image.sprite = _currentTemplate.Icon;
            _priceText.text = _price.ToString();
            _button.onClick.AddListener(Buy);
        }

        private void Buy()
        {
            if (_bag.HasCanSpend(_price))
            {
                _bag.Spend(_price);
                BuyAhead();
            }
            else
            {
                _priceText.color = Color.red;
                _priceText.DOColor(Color.white, 1f);
            }
        }

        public void BuyAhead(Action<Transform> callback = null)
        {
            var tower = NightPool.Spawn(_currentTemplate.Tower, _noBuilding.transform.position);
            tower.Initialization(_bag.Character,_noBuilding,_noBuilding.FreeIsland);
            tower.HealthComponent.OnHit += callback;
            var colorCharacter = _bag.Character.Color;
            
            _noBuilding.gameObject.SetActive(false);
            _noBuilding.FreeIsland.SetColor(colorCharacter,1f);
        }
    }
}