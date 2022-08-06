using System;
using System.Collections;
using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParameters
{
    public class DisplayHealth : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private HealthComponent _healthComponent;
        
        [Header("UI")]
        [SerializeField] private Image _sliderHp;
        [SerializeField] private TextMeshProUGUI _text;
        #region Enable / Disable
        private void OnEnable()
        {
            _healthComponent.OnUpdateHealth += UpdateHealthBar;
        }
        private void OnDisable()
        { 
            _healthComponent.OnUpdateHealth -= UpdateHealthBar;

        }
        #endregion
        private void Start()
        {
            if (_camera == false)
            {
                _camera = Camera.main;
            }
        }
        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
        
        private void UpdateHealthBar(int newHp)
        {
            _text.text = newHp.ToString();
            _sliderHp.DOFillAmount((float) newHp / _healthComponent.MaxCount, 0.3f);
        }
        
    }
}