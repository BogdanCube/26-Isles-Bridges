using Core.Characters._Base;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Recruit.Behavior
{
    public class StateRecruit : ScriptableObject,IState
    {
        public BehaviourRecruit BehaviourSystem;
        protected Character Recruit => BehaviourSystem.Recruit;
        protected IHealthComponent HealthComponent => Recruit.HealthComponent;
        protected MovementRecruit MovementController => BehaviourSystem.MovementController;
        protected AnimationStateController AnimationStateController => Recruit.AnimationStateController;
        protected DetectorFighting DetectorFighting => Recruit.DetectorFighting;
        protected DetectorFighting DetectorCharacter => BehaviourSystem.DetectorCharacter;

        public bool IsAggresive => DetectorCharacter.CurrentTarget && DetectorCharacter.CurrentTarget.IsActive();
        public virtual void Start()
        {
            AnimationStateController.IsRunning = true;
        }

        public virtual void Update()
        {
            if (HealthComponent.IsDeath)
            {
                BehaviourSystem.SetState(CreateInstance<DeathStateRecruit>());
            }
            else
            {
                if (DetectorFighting.IsFight)
                {
                    BehaviourSystem.SetState(CreateInstance<FightingStateRecruit>());
                }
                else
                {
                    if (IsAggresive)
                    {
                        BehaviourSystem.SetState(CreateInstance<AggressiveStateRecruit>());
                    }
                    else
                    {
                        LateUpdate();
                    }
                }
            }
        }

        public virtual void End() { }
        
        protected virtual void LateUpdate() { }

    }
}