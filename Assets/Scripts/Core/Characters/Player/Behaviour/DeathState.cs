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
            BehaviourSystem.Character.MovementController.Stop();
        }

        public override void Update()
        {
            
        }
    }
}