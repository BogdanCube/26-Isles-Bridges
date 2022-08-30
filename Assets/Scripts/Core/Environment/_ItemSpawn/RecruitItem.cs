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
        [SerializeField] private GrayscaleModel _grayscaleModel;
        private readonly int _runningNameId = Animator.StringToHash("IsRunning");
        public override void SetSpawner(Spawner spawner, Vector3 position)
        {
            _grayscaleModel.FadeToDefault(0);
            base.SetSpawner(spawner, new Vector3(0,0,-0.75f));
            
            transform.DOLookAt(position,1);
            transform.DOLocalMove(position, _speed).SetSpeedBased().OnComplete(() =>
            {
                _animator.SetBool(_runningNameId, false);
            });
        }

        public void PickUp(Action callback)
        {
            _grayscaleModel.FadeGray(1);
            transform.DOShakeScale(1f, Vector3.one/2f).OnComplete((() =>
            {
                transform.DOScale(0, 1).OnComplete(() =>
                {
                    callback.Invoke();
                    SpendCount();
                });
            }));
         
        }
    }
}