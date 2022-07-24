using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class DisplayProgressTower : MonoBehaviour
    {
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private TextMeshProUGUI _text;
        private float _duration = 3;
        private void OnEnable()
        {
            _towerLevel.OnUpdateDisplayed += UpdateDisplayed;
            _towerLevel.OnMaxUpgrade += MaxUpgrade;
        }
        
        private void OnDisable()
        {
            _towerLevel.OnUpdateDisplayed -= UpdateDisplayed;
            _towerLevel.OnMaxUpgrade -= MaxUpgrade;
        }
        
        private void UpdateDisplayed(int shopMoney, int priceNextLevel)
        {
            _text.text = $"{shopMoney}/{priceNextLevel}";
        }
        private void MaxUpgrade()
        {
            _text.text = "MAX";
            _text.DOColor(Color.red,_duration);
        }
    }
}