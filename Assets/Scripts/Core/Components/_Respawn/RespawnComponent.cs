using Base.Level;
using Core.Characters._Base;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Components._Respawn
{
    public class RespawnComponent : RespawnRecruit
    {
        [SerializeField] private BehaviourSystem _behaviourSystem;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AnimationStateController _animationState;
        [SerializeField] private BagCharacter _bag;
        [SerializeField] private ParticleSystem _particle;
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
        }
        protected override void Respawn()
        {
            _bag.Reset();
            _animationState.Live();
            _movementController.SetStartPos(_startPos);
            _particle.Activate();
            transform.DOScale(1, 0.75f).OnComplete(() =>
            {
                _behaviourSystem.IsStop = false;
                _behaviourSystem.SetIdleState();
                LoaderLevel.Instance.UpdateBake();
            });
         
        }
    }
}