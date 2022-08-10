using System;
using Base.Level;
using DG.Tweening;
using Managers.Level;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment.Bridge.Brick
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private GameObject _brick;
        private bool _isSet = false;
        public bool IsSet => _isSet;
        private void Start()
        {
            _brick.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.localPosition = position;
            transform.localScale = Vector3.one; 
        }

        [Button]
        public void SetBrick()
        {
            _brick.SetActive(true);
            _isSet = true;
            LoaderLevel.Instance.UpdateBake();
        }

        
    }
}