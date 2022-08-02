using UnityEngine;

namespace Core.Characters.Base.Behavior
{
    public abstract class State : ScriptableObject
    {
        public BehaviourSystem BehaviourSystem;
        public virtual void Start() { } 
        public virtual void Update() { } 

        public virtual void End() { }
    }
}
