using Core.Characters.Player;
using Core.Characters.Player.Behavior;
using UnityEngine;
namespace Core.Characters.Enemy.Behaviour
{
    public class IdleStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = false;
        }

        protected override void LateUpdate()
        {
            if (IsBuild)
            {
                BehaviourSystem.SetState(CreateInstance<BuildStateEnemy>());
            }
            else if (IsBuildTower)
            {
                BehaviourSystem.SetState(CreateInstance<BuildTowerStateEnemy>());
            }
            else if(IsCollection)
            {
                BehaviourSystem.SetState(CreateInstance<CollectionBrickStateEnemy>());
            }
            else if (IsNonState)
            {
                BehaviourSystem.SetState(CreateInstance<RunningStateEnemy>());
            }
        }
    }
}