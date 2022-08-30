using Core.Characters.Base.Behavior;
using UnityEngine;

namespace Core.Character.Behavior
{
    public class DeathState : State
    {
        public override void Start()
        {
            BehaviourSystem.Player.AnimationStateController.Death();
            BehaviourSystem.IsStop = true;
            BehaviourSystem.Player.MovementController.IsStopped = true;
        }
    }
}