using UnityEngine;
using DG.Tweening;
using Toolkit.Extensions;

namespace Core.Characters.Enemy.Behaviour
{
    public class DeathStateEnemy : StateEnemy
    {
        public override void Start()
        {
            BehaviourSystem.IsStop = true;
            Enemy.transform.DORotate(new Vector3(0, 180, 0), 1);
            MovementController.IsStopped = true;
            AnimationStateController.Dance();
            Enemy.WinParticle.Activate();
        }
    }
}