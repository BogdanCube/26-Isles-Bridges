using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Environment.Tower
{
    public class DisplayTower : MonoBehaviour
    {
        [FormerlySerializedAs("towerLevel")] [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private TextMeshProUGUI _text;
        private float _duration = 3;
        
        #region Enable/Disable
        private void OnEnable()
        {
            _towerLevel.OnUpdate += UpdateDisplayed;
            _towerLevel.OnHit += UpdateHit;
            _towerLevel.OnMaxUpgrade += MaxUpgrade;
        }
        
        private void OnDisable()
        {
            _towerLevel.OnUpdate -= UpdateDisplayed;
            _towerLevel.OnHit -= UpdateHit;
            _towerLevel.OnMaxUpgrade -= MaxUpgrade;
        }
        #endregion
        
        private void UpdateDisplayed(int shopMoney, int priceNextLevel,bool isReserve)
        {
            if (isReserve == false)
            {
                _text.text = $"{shopMoney}/{priceNextLevel}";
            }
            else
            {
                _text.text = $"{shopMoney}";
            }
        }
        private void UpdateHit(int shopMoney, int priceNextLevel,bool isReserve)
        {
            UpdateDisplayed(shopMoney, priceNextLevel,isReserve);
            transform.DOKill();
            _text.DOColor(_towerLevel.IsMaxLevel ? Color.red : Color.white, 1f);
            _text.color = _towerLevel.IsMaxLevel ? Color.white : Color.red;
        }
        private void MaxUpgrade()
        {
            _text.DOColor(Color.red,_duration);
        }
    }
}