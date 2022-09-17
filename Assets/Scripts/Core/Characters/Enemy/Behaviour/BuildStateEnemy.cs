namespace Core.Characters.Enemy.Behaviour
{
    public class BuildStateEnemy : StateEnemy
    {
        protected override void LateUpdate()
        {
            if (IsBuild)
            {
                if (FinderOutside.IsNoBuilding && DataTowers.CanBuySomething)
                {
                    MovementController.SetCheckTarget(FinderOutside.NoBuilding);
                }
                if (FinderOutside.IsBrick)
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