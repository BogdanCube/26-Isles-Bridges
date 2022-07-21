using System;
using UnityEngine;

namespace Core.Environment.Island
{
    public class Island : MonoBehaviour
    {
        [Range(0,25)] [SerializeField] private float _radius;
        [SerializeField] private float _coefficient;
        [SerializeField] private float _coefficientCollider;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private Transform _inside;
        [SerializeField] private Transform _outside;

        private void OnValidate()
        {
            _inside.localScale = new Vector3(_radius, _inside.localScale.y, _radius);
            
            var outRadius = _coefficient * _radius;
            _outside.localScale = new Vector3(outRadius, _outside.localScale.y, outRadius);

            _sphereCollider.radius = _coefficientCollider * _radius;;
        }
    }
}