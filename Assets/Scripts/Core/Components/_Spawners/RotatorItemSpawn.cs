using System;
using DG.Tweening;
using UnityEngine;

namespace Core.Components._Spawners
{
    public class RotatorItemSpawn : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationVector = new Vector3(0, 360, 0);
        [SerializeField] private Transform _model;
        [SerializeField] private float _speed;
        private void Start()
        {
            _model.DOLocalRotate(_rotationVector, _speed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);;
        }
    }
}