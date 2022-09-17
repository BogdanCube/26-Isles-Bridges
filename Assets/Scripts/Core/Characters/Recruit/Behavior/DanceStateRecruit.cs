using DG.Tweening;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Recruit.Behavior
{
    public class DanceStateRecruit : StateRecruit
    {
        public override void Start()
        {
            BehaviourSystem.IsStop = true;
            BehaviourSystem.transform.DORotate(new Vector3(0, 180, 0), 1);
            MovementController.IsStopped = true;
            AnimationStateController.Dance();
            Recruit.WinParticle.Activate();
        }
    }
}