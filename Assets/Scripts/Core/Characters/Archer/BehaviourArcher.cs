using System;
using Core.Characters._Base;
using Core.Characters.Archer.Behaviour;
using NTC.Global.Cache;
using UnityEngine;

namespace Core.Characters.Archer
{
    public class BehaviourArcher : BehaviourSystem
    {
        [SerializeField] private StateArcher _currentState;
        [SerializeField] private Character _archer;
        protected override IState CurrentState => _currentState;
        public Character Archer => _archer;
        
        public void SetState(StateArcher state)
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
            SetState(ScriptableObject.CreateInstance<IdleStateArcher>());
        }
    }
}