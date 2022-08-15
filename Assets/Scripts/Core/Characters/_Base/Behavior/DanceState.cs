using Core.Characters.Base.Behavior;
using DG.Tweening;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Player.Behaviour
{
    public class DanceState : State
    {
        public override void Start()
        {
            BehaviourSystem.IsStop = true;
            BehaviourSystem.transform.DORotate(new Vector3(0, 180, 0), 1);
            BehaviourSystem.Character.MovementController.IsStopped = true;
            BehaviourSystem.Character.AnimationStateController.Dance();
            BehaviourSystem.Character.WinParticle.Activate();
        }
    }
}