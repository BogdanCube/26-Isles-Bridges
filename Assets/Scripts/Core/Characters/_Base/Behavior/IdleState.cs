using Core.Characters.Base.Behavior;
using UnityEngine;

namespace Core.Character.Behavior
{
    public class IdleState : State
    {
        public override void Start()
        {
            BehaviourSystem.Character.AnimationStateController.IsRunning = false;
        }
    }
}