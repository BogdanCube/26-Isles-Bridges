namespace Core.Characters.Enemy.Behaviour
{
    public class BuildStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }
        public override void UpdateAction()
        {
            if (Enemy.IsBuild)
            {
                if (FinderOutside.IsNoBuilding && DataTowers.CanBuySomething)
                {
                    MovementController.SetCheckTarget(FinderOutside.NoBuilding);
                }
                else if (FinderOutside.IsBrick)
                {
                    MovementController.SetTarget(FinderOutside.Brick);
                }
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}