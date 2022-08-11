using System;
using Core.Components._Spawners;
using DG.Tweening;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment._ItemSpawn
{
    public class RecruitItem : ItemSpawn
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _speed;
        [SerializeField] private Collider _collider;
        [SerializeField] private GrayscaleModel _grayscaleModel;
        [SerializeField] private float _timeFade = 3;
        private readonly int _runningNameId = Animator.StringToHash("IsRunning");
        public override void SetSpawner(Spawner spawner, Vector3 position)
        {
            base.SetSpawner(spawner, new Vector3());
            _collider.enabled = false;
            _grayscaleModel.FadeGray(0);
            _animator.SetBool(_runningNameId, true);
            transform.DOLookAt(position, 1f);
            transform.DOLocalMove(position, _speed).SetSpeedBased().SetEase(Ease.Flash).OnComplete(() =>
            {
                _animator.SetBool(_runningNameId, false);
                _collider.enabled = true;
            });
        }

        public void PickUp(Action callback)
        {
            transform.DOShakeScale(1f, Vector3.one/10f);
            _grayscaleModel.FadeToDefault(1).SetEase(Ease.Flash).OnComplete(() =>
            {
                transform.DOScale(0, 1).OnComplete(() =>
                    {
                        callback.Invoke();
                        NightPool.Despawn(transform);
                    });
            });
        }
    }
}