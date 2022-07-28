using System;
using Core.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Environment.Island
{
    public class FreeIsland : Island
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isDelight;
        public event Action OnDelightIsland;
        public bool IsDelight => _isDelight;
        private void Start()
        {
            if (_isDelight == false)
            {
                SetColor(Color.gray, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isDelight == false && other.TryGetComponent(out Characters.Base.Character character))
            {
                _isDelight = true;
                OnDelightIsland?.Invoke();
                SetColor(character.Color,_duration);
            }
        }

        public void SetColor(Color color, float duration)
        {
            _meshRenderer.material.DOColor(color,duration);
        }
    }
}