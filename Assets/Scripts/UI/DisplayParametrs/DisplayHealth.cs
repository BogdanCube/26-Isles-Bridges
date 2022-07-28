using System;
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
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _sliderHp;
        
        [Space(5)][SerializeField] private HealthComponent _healthComponent;
        
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
        }
        
        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
        private void HideBar()
        {
            gameObject.SetActive(false);
        }
       
        
        private void UpdateHealthBar(int newHp)
        {
            _text.text = newHp.ToString();
            _sliderHp.DOFillAmount((float) newHp / _healthComponent.MaxCount, 0.3f);
        }
        
    }
}