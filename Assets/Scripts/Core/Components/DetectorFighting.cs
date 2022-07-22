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
        
        protected Components.Behavior.Character _currentTarget;
        public bool IsFight => _currentTarget != null;
        public Components.Behavior.Character CurrentTarget => _currentTarget;
        
        private void OnValidate()
        {
            _sphereCollider.radius = _radius;
            _outline.transform.localScale = new Vector2(_radius, _radius);
            _outline.enabled = _isDebuged;
        }
    }
}