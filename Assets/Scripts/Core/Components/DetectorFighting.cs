using Core.Components._ProgressComponents.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Components
{
    public class DetectorFighting : MonoBehaviour
    {
        [Range(0,12)] [SerializeField]  private float _radius;
        [SerializeField] private bool _isDebuged;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private Image _outline;
        [SerializeField] private protected bool _isNullExit = true;
        
        protected Transform _currentTarget;
        protected IHealthComponent _currentHealth;
        public Transform CurrentTarget => _currentTarget;
        public IHealthComponent CurrentHealth => _currentHealth;
        public bool IsFight => _currentTarget != null && CurrentHealth.IsDeath == false;

        
        private void OnValidate()
        {
            _sphereCollider.radius = _radius;
            _outline.transform.localScale = new Vector2(_radius, _radius);
            _outline.enabled = _isDebuged;
        }
    }
}