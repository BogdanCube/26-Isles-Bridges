using Core.Environment.Tower;
using Core.Environment.Tower._Base;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.DisplayParameters
{
    public class DisplayHealthTower : MonoBehaviour
    {
        [SerializeField] private HealthTower _healthTower;

        [SerializeField] private Image _background;
        [FormerlySerializedAs("_sliderHp")] [SerializeField] private Image _slider;
        [SerializeField] private TextMeshProUGUI _text;
        
        #region Enable / Disable
        private void OnEnable()
        {
            _healthTower.OnUpdateHealth += UpdateHealthBar;
            _healthTower.OnHit += UpdateHit;
        }
        private void OnDisable()
        { 
            _healthTower.OnUpdateHealth -= UpdateHealthBar;
            _healthTower.OnHit -= UpdateHit;
        }
        #endregion

        private void UpdateHealthBar(int currentCount, int maxCount)
        {
            _text.text = currentCount.ToString();
            var correlation = (float)currentCount / maxCount;
            _slider.DOFillAmount(correlation, 0.3f);
        }

        private void UpdateHit(Transform transform)
        {
            _text.DOColor(Color.white, 1f);
            _text.color = Color.red;
            _background.transform.DOShakeScale(1f, Vector3.one / 10f);
        }
    }
}