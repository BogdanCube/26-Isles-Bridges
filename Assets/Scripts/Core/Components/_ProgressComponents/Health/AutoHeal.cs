using System;
using System.Collections;
using NaughtyAttributes;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Components._ProgressComponents.Health
{
    public class AutoHeal : MonoBehaviour
    {
        [MinMaxSlider(0,10)][SerializeField] private Vector2 _timeStartHealing = new(5,10);
        [MinMaxSlider(0,10)][SerializeField] private Vector2 _timeCancelHit = new(4,7);
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private DetectorFighting _detectorFighting;
        private bool _isHit;
        private IEnumerator _cancelHit;
        
        private void OnEnable()
        {
            _healthComponent.OnHit += CheckHit;
        }

        private void OnDisable()
        {
            _healthComponent.OnHit -= CheckHit;
        }

        private void Start()
        {
            StartCoroutine(HealCoroutine());
        }
        private void CheckHit()
        {
            if (_isHit == false)
            {
                StartCoroutine(CancelHit());
            }
           
        }
        private IEnumerator CancelHit()
        {
            _isHit = true;
            yield return new WaitForSeconds(_timeCancelHit.RandomRange());
            _isHit = false;
        }
        private IEnumerator HealCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeStartHealing.RandomRange());

                while (IsHeal())
                {
                    _healthComponent.Heal();
                    yield return new WaitForSeconds(0.1f);
                }
            }

            bool IsHeal()
            {
                return _detectorFighting.IsFight == false
                       && _isHit == false
                       && _healthComponent.IsFull == false;
            }
        }
    }
}