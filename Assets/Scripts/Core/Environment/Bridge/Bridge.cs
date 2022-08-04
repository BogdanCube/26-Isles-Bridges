using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Bridge
{
    public class Bridge : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private int _poolCount;
        
        public event Action<int> OnBuild;
        public float Offset => _offset;
        private void Start()
        {
            OnBuild?.Invoke(_poolCount);
        }
    }
}