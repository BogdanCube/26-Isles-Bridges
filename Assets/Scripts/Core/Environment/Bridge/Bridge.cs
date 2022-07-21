using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Bridge
{
    public class Bridge : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private int _poolCount;
        
        [Space][SerializeField] private int _countBrick;
        [SerializeField] private int _maxCount;
        [SerializeField] private Island.Island _builtTo;

        public event Action<int> OnBuild;
        public float Offset => _offset;
        private void Start()
        {
            CountedMaxCount();
            OnBuild?.Invoke(_maxCount);
        }

        private void CountedMaxCount()
        {
            Transform island = _builtTo.transform;
            transform.LookAt(island);
            
            var distance = Vector3.Distance(transform.position, island.transform.position);
            _maxCount = (int)Math.Ceiling(distance / _offset) + _poolCount;
        }
    }
}