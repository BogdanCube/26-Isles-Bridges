using System;
using Core.Environment.Tower._Base;
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
        }
        
        private void OnDisable()
        {
            _towerLevel.OnUpdate -= UpdateDisplayed;
        }
        #endregion
        
        private void UpdateDisplayed(int shopMoney, int priceNextLevel)
        {
            _text.text = $"{shopMoney}/{priceNextLevel}";

        }
    }
}