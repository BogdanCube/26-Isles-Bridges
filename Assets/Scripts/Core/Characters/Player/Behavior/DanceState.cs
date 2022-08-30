using Core.Characters.Base.Behavior;
using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Player.Behavior
{
    public class DanceState : State
    {
        public override void Start()
        {
            BehaviourSystem.IsStop = true;
            BehaviourSystem.transform.DORotate(new Vector3(0, 180, 0), 1);
            BehaviourSystem.Player.MovementController.IsStopped = true;
            BehaviourSystem.Player.AnimationStateController.Dance();
            BehaviourSystem.Player.WinParticle.Activate();
        }
    }
}