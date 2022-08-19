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
        private Collider _collider;
        public bool IsSet => !_collider.enabled;
        private void Start()
        {
            _brick.SetActive(false);
            _collider = GetComponent<Collider>();
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
            _collider.enabled = false;
            GetComponent<Brick>().enabled = false;
        }
    }
}