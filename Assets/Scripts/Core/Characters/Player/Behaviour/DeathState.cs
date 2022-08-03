using Core.Characters.Base.Behavior;
using UnityEngine;

namespace Core.Character.Behavior
{
    public class DeathState : State
    {
        public override void Start()
        {
            BehaviourSystem.Character.AnimationStateController.Death();
            BehaviourSystem.IsStop = true;
            BehaviourSystem.Character.MovementController.IsStopped = true;
        }

        public override void Update()
        {
            if (BehaviourSystem.Character.HealthComponent.IsDeath == false)
            {
                BehaviourSystem.IsStop = false;
                BehaviourSystem.Character.MovementController.IsStopped = false;
                BehaviourSystem.Character.AnimationStateController.Live();
            }
        }
    }
}