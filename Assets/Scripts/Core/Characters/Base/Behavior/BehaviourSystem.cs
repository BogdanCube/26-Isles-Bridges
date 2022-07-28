using Core.Character.Behavior;
using UnityEngine;

namespace Core.Characters.Base.Behavior
{
    public abstract class BehaviourSystem : MonoBehaviour 
    {
        [SerializeField] private protected State _currentState;
        [SerializeField] private protected Character _character;
        [HideInInspector] public bool IsStop { private get; set; }
        public Character Character => _character;

        protected void SetState(State state)
        {
            if (IsStop == false)
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
}