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
        
        public override void UpdateAction()
        {
            if (Enemy.IsBuild)
            {
                BehaviourSystem.SetState(CreateInstance<BuildStateEnemy>());
            }
            else if (Enemy.IsBuildTower)
            {
                BehaviourSystem.SetState(CreateInstance<BuildTowerStateEnemy>());
            }
            else if(Enemy.IsCollection)
            {
                BehaviourSystem.SetState(CreateInstance<CollectionBrickStateEnemy>());
            }
            else if(Enemy.IsNonState)
            {
                BehaviourSystem.SetState(CreateInstance<RunningStateEnemy>());
            }
        }
    }
}