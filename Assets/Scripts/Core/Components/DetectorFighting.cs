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
        
        protected IHealthComponent _currentTarget;
        public bool IsFight => _currentTarget != null && _currentTarget.IsDeath == false;
        public IHealthComponent CurrentTarget => _currentTarget;
        
        private void OnValidate()
        {
            _sphereCollider.radius = _radius;
            _outline.transform.localScale = new Vector2(_radius, _radius);
            _outline.enabled = _isDebuged;
        }
    }
}