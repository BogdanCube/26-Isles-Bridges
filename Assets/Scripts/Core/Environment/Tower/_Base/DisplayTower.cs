using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class DisplayTower : MonoBehaviour
    {
        [SerializeField] private TowerLevel towerLevel;
        [SerializeField] private TextMeshProUGUI _text;
        private float _duration = 3;
        
        #region Enable/Disable
        private void OnEnable()
        {
            towerLevel.OnUpdateDisplayed += UpdateDisplayed;
            towerLevel.OnHitDisplayed += UpdateHit;
            towerLevel.OnMaxUpgrade += MaxUpgrade;
        }
        
        private void OnDisable()
        {
            towerLevel.OnUpdateDisplayed -= UpdateDisplayed;
            towerLevel.OnHitDisplayed -= UpdateHit;
            towerLevel.OnMaxUpgrade -= MaxUpgrade;
        }
        #endregion
        
        private void UpdateDisplayed(int shopMoney, int priceNextLevel)
        {
            _text.color = Color.white;
            _text.text = $"{shopMoney}/{priceNextLevel}";
        }
        private void UpdateHit(int shopMoney, int priceNextLevel)
        {
            UpdateDisplayed(shopMoney, priceNextLevel);
            transform.DOKill();
            _text.DOColor(Color.white, 1f);
            _text.color = Color.red;
        }
        private void MaxUpgrade()
        {
            _text.text = "MAX";
            _text.DOColor(Color.red,_duration);
        }
    }
}