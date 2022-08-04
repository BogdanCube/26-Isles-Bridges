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

        public bool IsSet => _brick.activeSelf;
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
            LoaderLevel.Instance.UpdateBake();
        }

        
    }
}