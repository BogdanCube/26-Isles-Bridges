using System;
using UnityEngine;

namespace Core.Components.GrayscaleEffect
{
    public class GrayMaterial : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int _index;
        public Material Material => _renderer.materials[_index];

        private void Start()
        {
            var material = new Material(_renderer.materials[_index]);
        }
    }
}