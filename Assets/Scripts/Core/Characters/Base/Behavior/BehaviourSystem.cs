using Core.Character.Behavior;
using UnityEngine;

namespace Core.Characters.Base.Behavior
{
    public abstract class BehaviourSystem : MonoBehaviour 
    {
        [SerializeField] private protected State _currentState;
        [SerializeField] private protected Character _character;

        public Character Character => _character;

        protected void SetState(State state)
        {
            if (_currentState != null) {
                _currentState.End();
            }
            _currentState = Instantiate(state);
            _currentState.BehaviourSystem = this;
            _currentState.Start();
        }
    }
}