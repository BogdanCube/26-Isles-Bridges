using Core.Characters._Base;
using Core.Components;
using UnityEngine;

namespace Core.Characters.Archer.Behaviour
{
    public abstract class StateArcher : ScriptableObject,IState
    {
        public BehaviourArcher BehaviourSystem;
        protected Character Archer => BehaviourSystem.Archer;
        protected AnimationStateController AnimationStateController => Archer.AnimationStateController;
        protected DetectorFighting DetectorFighting => Archer.DetectorFighting;
        public virtual void Start() { } 
        public virtual void Update() { } 

        public virtual void End() { }
    }
}