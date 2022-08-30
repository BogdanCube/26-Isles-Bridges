using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.DisplayParameters
{
    public class DisplayHealth : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private HealthComponent _healthComponent;
        
        [Header("UI")]
        [SerializeField] private Image _background;
        [FormerlySerializedAs("_sliderHp")] [SerializeField] private Image _slider;
        [SerializeField] private TextMeshProUGUI _text;
        #region Enable / Disable
        private void OnEnable()
        {
            _healthComponent.OnUpdateHealth += UpdateHealthBar;
            _healthComponent.OnDeath += Hide;
            _healthComponent.OnRespawn += Show;
        }
        private void OnDisable()
        { 
            _healthComponent.OnUpdateHealth -= UpdateHealthBar;
            _healthComponent.OnDeath += Hide;
            _healthComponent.OnRespawn -= Show;
        }
        #endregion
        private void Start()
        {
            if (_camera == false)
            {
                _camera = Camera.main;
            }
        }

        private void Hide()
        {
            _background.DOFade(0, 1);
            _slider.DOFade(0, 1);
            _text.DOFade(0, 1);
        }

        private void Show()
        {
            _background.DOFade(1, 1);
            _slider.DOFade(1, 1);
            _text.DOFade(1, 1);
        }
        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
        
        private void UpdateHealthBar(int newHp)
        {
            _text.text = newHp.ToString();
            _slider.DOFillAmount((float) newHp / _healthComponent.MaxCount, 0.3f);
        }
        
    }
}