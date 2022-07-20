using System;
using System.Collections;
using UnityEngine;

namespace Components.Health
{
    public class RegenerationHealth : MonoBehaviour
    {
        [SerializeField] private float _intervalRegeneration;
        [SerializeField] private HealthComponent _healthComponent;
        
        private float _regenerationHealth;
        
        private void OnEnable()
        {
            _healthComponent.OnRepeatHealEvent += OnRepeatHeal;
        }
        private void OnDisable()
        {
            _healthComponent.OnRepeatHealEvent -= OnRepeatHeal;
        }
        
        private void OnRepeatHeal()
        {           
            _regenerationHealth = _healthComponent.MaxHealth / 10;
            StartCoroutine(RepeatHeal());
        }
        private IEnumerator RepeatHeal()
        {
            while (_healthComponent.IsHalfHealth)
            {
                yield return new WaitForSeconds(_intervalRegeneration);
                _healthComponent.Heal(_regenerationHealth);
            }
        }
    }
}