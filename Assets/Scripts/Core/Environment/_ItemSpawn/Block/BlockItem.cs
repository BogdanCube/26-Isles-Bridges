using Core.Components._Spawners;
using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Environment._ItemSpawn.Block
{
    public class BlockItem : ItemSpawn
    {
        [SerializeField] private float _startHeight = 2;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _timeInUp = 1.25f;
        [SerializeField] private float _timeGravity = 0.55f;

        public override void SetSpawner(Spawner spawner, Vector3 position)
        {
            base.SetSpawner(spawner, new Vector3(0,_startHeight,0));
            _collider.enabled = false;
            
            transform.DOScale(Vector3.one / 2, 0);
            transform.DOScale(Vector3.one, _timeGravity);
            
            transform.DOLocalMoveArc(position,_timeInUp,_timeGravity).OnComplete(() =>
            {
                _collider.enabled = true;
            });
        }
    }
}