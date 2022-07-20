using UnityEngine;

namespace Components.Health
{
    public class ParticleHealth : MonoBehaviour
    {
        [SerializeField] protected HealthComponent _healthComponent;

        [Header("Particle")] 
        [SerializeField] private ParticleSystem _particleHit;
        [SerializeField] private ParticleSystem _particleDeath;
        
        private void OnEnable()
        {
            _healthComponent.OnHitEvent += OnPlayedHit;
            _healthComponent.OnDeath += OnPlayedDeath;
        }

        private void OnDisable()
        {
            _healthComponent.OnHitEvent -= OnPlayedHit;
            _healthComponent.OnDeath -= OnPlayedDeath;
        }

        private void OnPlayedHit(float newHp)
        {
            _particleHit.gameObject.SetActive(true);
        }
        
        private void OnPlayedDeath()
        {
            _particleDeath.gameObject.SetActive(true);
        }
    }
}