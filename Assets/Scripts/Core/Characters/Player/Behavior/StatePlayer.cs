using Core.Characters._Base;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Characters.Player.Behavior
{
    public abstract class StatePlayer : ScriptableObject,IState
    {
        public BehaviourPlayer BehaviourSystem;
        protected Player Player => BehaviourSystem.Player;
        protected IHealthComponent HealthComponent => Player.HealthComponent;
        protected PlayerMovement MovementController => Player.MovementController;
        protected AnimationStateController AnimationStateController => Player.AnimationStateController;
        protected DetectorFighting DetectorFighting => Player.DetectorFighting;
        public virtual void Start() { } 
        public virtual void Update() { } 

        public virtual void End() { }
        
    }
}
