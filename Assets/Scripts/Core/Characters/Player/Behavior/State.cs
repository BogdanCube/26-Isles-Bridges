using Core.Characters._Base;
using Core.Characters.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Characters.Base.Behavior
{
    public abstract class State : ScriptableObject,IState
    {
        public BehaviourPlayer BehaviourSystem;
        public virtual void Start() { } 
        public virtual void Update() { } 

        public virtual void End() { }
    }
}
