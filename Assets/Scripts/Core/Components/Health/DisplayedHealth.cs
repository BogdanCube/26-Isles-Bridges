using System;
using Core.Character;
using Core.Character.Behavior;
using Core.Components.Behavior;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Health
{
    public class DisplayedHealth : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _sliderHp;
        
        [Space(5)][SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private BehaviourSystem _behaviourSystem;

        private void OnEnable()
        {
            _healthComponent.OnHitEvent += OnHit;
            _healthComponent.OnHealEvent += OnHeal;
            _healthComponent.OnDeath += OnDeath;
        }
        
        private void OnDisable()
        { 
            _healthComponent.OnHitEvent -= OnHit;
            _healthComponent.OnHealEvent -= OnHeal;
            _healthComponent.OnDeath -= OnDeath;
        }

        private void Start()
        {
            UpdateHealthBar(_healthComponent.MaxHealth);
        }

        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
        }
        private void OnHit(float newHp)
        {
            UpdateHealthBar(newHp);
        }

        private void OnHeal(float newHp)
        {
            UpdateHealthBar(newHp);
        }
        private void OnDeath()
        {
            Destroy(_behaviourSystem.gameObject,0.5f);
        }

        private void UpdateHealthBar(float newHp)
        {
            _text.text = Math.Round(newHp).ToString();
            _sliderHp.DOFillAmount((float) newHp / _healthComponent.MaxHealth, 0.3f);
        }
        
    }
}