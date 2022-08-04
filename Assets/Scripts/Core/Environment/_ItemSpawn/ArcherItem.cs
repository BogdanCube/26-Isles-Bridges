using Core.Components._Spawners;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment._ItemSpawn
{
    public class ArcherItem : ItemSpawn
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _speed;
        private readonly int _runningNameId = Animator.StringToHash("IsRunning");

        public override void SetSpawner(Spawner spawner, Vector3 position)
        {
            base.SetSpawner(spawner, new Vector3());
            _animator.SetBool(_runningNameId, true);
            transform.DOLookAt(position, 1f);
            transform.DOLocalMove(position, _speed).SetSpeedBased().SetEase(Ease.Flash).OnComplete(() =>
            {
                _animator.SetBool(_runningNameId, false);
            });
        }
    }
}