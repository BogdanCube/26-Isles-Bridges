namespace Core.Characters.Enemy.Behaviour
{
    public class BuildTowerStateEnemy : StateEnemy
    {
        protected override void LateUpdate()
        {
            if (IsBuildTower)
            {
                MovementController.SetTarget(FinderTower.Tower);
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}