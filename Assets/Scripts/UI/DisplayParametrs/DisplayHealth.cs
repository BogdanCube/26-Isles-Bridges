using System;
using System.Collections;
using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParametrs
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
            _healthComponent.OnDeath += HideBar;
        }
        private void OnDisable()
        { 
            _healthComponent.OnUpdateHealth -= UpdateHealthBar;
            _healthComponent.OnDeath -= HideBar;

        }
        #endregion
        private void Start()
        {
            if (_camera == false)
            {
                _camera = Camera.main;
            }
            HideBar();
        }
        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
       
        public void HideBar()
        {
           gameObject.SetActive(false);
        }
        public void ShowBar()
        {
            gameObject.SetActive(true);
            _text.text = _healthComponent.CurrentCount.ToString();
        }
        
        private void UpdateHealthBar(int newHp)
        {
            _text.text = newHp.ToString();
            _sliderHp.DOFillAmount((float) newHp / _healthComponent.MaxCount, 0.3f);
        }
        
    }
}