using System;
using Core.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Environment.Island
{
    public class FreeIsland : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isFree;

        private void Start()
        {
            if (_isFree)
            {
                SetColor(Color.gray, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isFree && other.TryGetComponent(out Components.Behavior.Character character))
            {
                _isFree = false;
                SetColor(character.Color,_duration);
            }
        }

        private void SetColor(Color color, float duration)
        {
            _meshRenderer.material.DOColor(color,duration);
        }
    }
}