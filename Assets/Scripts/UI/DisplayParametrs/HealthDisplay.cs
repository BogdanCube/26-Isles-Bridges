using System;
using Core.Character;
using Core.Character.Behavior;
using Core.Components.Behavior;
using Core.Components.Health;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Health
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _sliderHp;
        
        [Space(5)][SerializeField] private HealthComponent _healthComponent;

        private void OnEnable()
        {
            _healthComponent.OnUpdateHealth += UpdateHealthBar;
        }
        
        private void OnDisable()
        { 
            _healthComponent.OnUpdateHealth -= UpdateHealthBar;
        }

        
        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
       
        
        private void UpdateHealthBar(int newHp)
        {
            _text.text = newHp.ToString();
            _sliderHp.DOFillAmount((float) newHp / _healthComponent.MaxCount, 0.3f);
        }
        
    }
}