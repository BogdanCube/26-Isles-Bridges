using UnityEngine;
using UnityEngine.UI;

namespace Core.Characters.Enemy.Finder
{
    public class FinderBase : MonoBehaviour
    {
        [Range(0,12)] [SerializeField]  private float _radius;
        [SerializeField] private bool _isDebuged;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private Image _outline;
        
        private void OnValidate()
        {
            _sphereCollider.radius = _radius;
            _outline.transform.localScale = new Vector2(_radius, _radius);
            _outline.enabled = _isDebuged;
        }
    }
}