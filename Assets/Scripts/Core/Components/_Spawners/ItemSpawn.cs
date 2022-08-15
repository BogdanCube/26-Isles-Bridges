using System;
using DG.Tweening;
using JetBrains.Annotations;
using NaughtyAttributes;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._Spawners
{
    public class ItemSpawn : MonoBehaviour
    {
        [MinMaxSlider(0, 360)] [SerializeField] private Vector2 _rotationY;
        [SerializeField] [CanBeNull] private TrailRenderer _trailRenderer;
        private float _speedMagnet = 6;
        private Spawner _spawner;
        private Vector3 _startScale;

        private void Start()
        {
            _startScale = transform.localScale;
        }

        public virtual void SetSpawner(Spawner spawner,Vector3 position)
        {
            if (_trailRenderer) _trailRenderer.enabled = false;
            _spawner = spawner;
            SetPosition(position);

            var randomRotation = _rotationY.RandomRange();
            transform.localRotation = Quaternion.Euler(0, randomRotation, 0);
        }

        private void SetPosition(Vector3 position)
        {
            transform.localPosition = position;

        }
        public void SpendCount()
        {
            _spawner.Spend();
            NightPool.Despawn(this);
        }

        public void MoveToCharacter(Transform character,Action callback = null)
        {
            if (_trailRenderer) _trailRenderer.enabled = true;
            transform.DOMove(character.position, _speedMagnet).SetSpeedBased().SetEase(Ease.Flash);
            transform.DOScale(0, _speedMagnet).SetSpeedBased().SetEase(Ease.Flash).OnComplete(() =>
            {
                transform.localScale = _startScale;
                callback?.Invoke();
                SpendCount();
            });
        }
    }
}