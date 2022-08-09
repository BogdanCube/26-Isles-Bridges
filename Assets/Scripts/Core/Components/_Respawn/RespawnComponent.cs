using Core.Character.Behavior;
using Core.Characters.Base.Behavior;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using UnityEngine;

namespace Core.Components
{
    public class RespawnComponent : RespawnRecruit
    {
        [SerializeField] private BehaviourSystem _behaviourSystem;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AnimationStateController _animationState;
        [SerializeField] private BagCharacter _bag;
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
            transform.DOScale(1, 0.2f).OnComplete(() =>
            {
                _behaviourSystem.IsStop = false;
            });
         
        }
    }
}