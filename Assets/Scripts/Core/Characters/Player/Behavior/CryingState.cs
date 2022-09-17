using DG.Tweening;
using UnityEngine;

namespace Core.Characters.Player.Behavior
{
    public class CryingState : StatePlayer
    {
        public override void Start()
        {
            BehaviourSystem.IsStop = true;
            BehaviourSystem.transform.DORotate(new Vector3(0, 180, 0), 1);
            MovementController.IsStopped = true; 
            AnimationStateController.Crying();
        }
    }
}