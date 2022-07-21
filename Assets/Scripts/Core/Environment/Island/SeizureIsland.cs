using System;
using Core.Components;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment.Island
{
    public class SeizureIsland : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isSeizure;

        private void Start()
        {
            if (_isSeizure == false)
            {
                SetColor(Color.gray, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isSeizure == false && other.TryGetComponent(out Components.Behavior.Character character))
            {
                _isSeizure = true;
                SetColor(character.Color,_duration);
            }
        }

        private void SetColor(Color color, float duration)
        {
            _meshRenderer.material.DOColor(color,duration);
        }
    }
}