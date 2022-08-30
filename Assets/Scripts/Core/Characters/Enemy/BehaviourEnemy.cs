using Core.Character.Behavior;
using Core.Characters._Base;
using Core.Characters.Base.Behavior;
using Core.Characters.Enemy.Behaviour;
using Core.Characters.Player.Behavior;
using NTC.Global.Cache;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class BehaviourEnemy : BehaviourSystem
    {
        [SerializeField] private StateEnemy _currentState;
        [SerializeField] private Enemy _enemy;
        protected override IState CurrentState => _currentState;
        public Enemy Enemy => _enemy;

        public void SetState(StateEnemy state)
        {
            if (_currentState != null) {
                _currentState.End();
            }
            _currentState = Instantiate(state);
            _currentState.BehaviourSystem = this;
            _currentState.Start();
        }


        protected override void SetIdleState()
        {
            SetState(ScriptableObject.CreateInstance<IdleStateEnemy>());
        }

        public void SetDanceState()
        {
            SetState(ScriptableObject.CreateInstance<DanceStateEnemy>());

        }
    }
}