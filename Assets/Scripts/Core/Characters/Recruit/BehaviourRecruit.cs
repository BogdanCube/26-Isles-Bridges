using Core.Characters._Base;
using Core.Characters.Recruit.Behavior;
using Core.Components;
using UnityEngine;

namespace Core.Characters.Recruit
{
    public class BehaviourRecruit : BehaviourSystem
    {
        [SerializeField] private StateRecruit _currentState;
        [SerializeField] private Character _recruit;
        [SerializeField] protected MovementRecruit _movementRecruit;
        [SerializeField] protected DetectorFighting _detectorCharacter;
        protected override IState CurrentState => _currentState;
        public Character Recruit => _recruit;
        public MovementRecruit MovementController => _movementRecruit;
        public DetectorFighting DetectorCharacter => _detectorCharacter;

        public void SetState(StateRecruit state)
        {
            if (_currentState != null) {
                _currentState.End();
            }
            _currentState = Instantiate(state);
            _currentState.BehaviourSystem = this;
            _currentState.Start();
        }
        
        public override void SetIdleState()
        {
            SetState(ScriptableObject.CreateInstance<IdleStateRecruit>());
        }
        public override void SetDanceState()
        {
            SetState(ScriptableObject.CreateInstance<DanceStateRecruit>());
        }
    }
}