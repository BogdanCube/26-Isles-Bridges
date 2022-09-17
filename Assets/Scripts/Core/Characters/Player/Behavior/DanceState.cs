using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Player.Behavior
{
    public class DanceState : StatePlayer
    {
        public override void Start()
        {
            BehaviourSystem.IsStop = true;
            BehaviourSystem.transform.DORotate(new Vector3(0, 180, 0), 1);
            MovementController.IsStopped = true; 
            AnimationStateController.Dance();
            Player.WinParticle.Activate();
        }
    }
}