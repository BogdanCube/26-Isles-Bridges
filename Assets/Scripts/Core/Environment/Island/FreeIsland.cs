using System;
using Core.Characters._Base;
using Core.Components;
using Core.Components._ProgressComponents.OwnerRecruit;
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

        private void Start()
        {
            if (_isDelight == false)
            {
                SetColor(Color.gray, 0);
            }
            OnDelightIsland?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isDelight == false && other.TryGetComponent(out Character character) && other.TryGetComponent(out DetachmentRecruit detachmentRecruit))
            {
                _isDelight = true;
                SetColor(character.Color,_duration);
            }
        }

        public void SetColor(Color color, float duration)
        {
            _meshRenderer.material.DOColor(color,duration);
        }
    }
}