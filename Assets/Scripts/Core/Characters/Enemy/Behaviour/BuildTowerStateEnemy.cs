namespace Core.Characters.Enemy.Behaviour
{
    public class BuildTowerStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }

        public override void UpdateAction()
        {
            if (Enemy.IsBuildTower)
            {
                MovementController.SetTarget(FinderOutside.Tower);
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}