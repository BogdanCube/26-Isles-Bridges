using Core.Components.Behavior;
using UnityEngine;

namespace Core.Character.Behavior
{
    public abstract class State : ScriptableObject
    {
        public BehaviourSystem BehaviourSystem;
        public virtual void Start() { } 
        public abstract void Update();

        public virtual void End() { }
    }
}
