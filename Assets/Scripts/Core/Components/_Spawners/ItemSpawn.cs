using System;
using DG.Tweening;
using NaughtyAttributes;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._Spawners
{
    [RequireComponent(typeof(TrailRenderer))]
    public class ItemSpawn : MonoBehaviour
    {
        [MinMaxSlider(0, 360)] [SerializeField] private Vector2 _rotationY;
        private TrailRenderer _trailRenderer;
        private float _speedMagnet = 6;
        private Spawner _spawner;
        private Vector3 _startScale;

        private void Start()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        public void SetSpawner(Spawner spawner,Vector3 position)
        {
            _spawner = spawner;
            transform.localPosition = position;
            _startScale = transform.localScale;

            //SetRandomAngle();
        }
        public void SpendCount()
        {
            _spawner.Spend();
            NightPool.Despawn(this);
        }

        public void MoveToCharacter(Transform character,Action callback)
        {
            _trailRenderer.enabled = true;
            transform.DOMove(character.position, _speedMagnet).SetSpeedBased().SetEase(Ease.Flash);
            transform.DOScale(0, _speedMagnet).SetSpeedBased().SetEase(Ease.Flash).OnComplete(() =>
            {
                _trailRenderer.enabled = false; 
                transform.localScale = _startScale;
                transform.Deactivate();

                callback.Invoke();
            });
        }
        protected void SetRandomAngle()
        {
            var randomRotation = _rotationY.RandomRange();
            transform.localRotation = Quaternion.Euler(0, randomRotation, 0);
        }
    }
}