namespace Core.Characters.Enemy.Behaviour
{
    public class BuyProgressStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }

        public override void UpdateAction()
        {
            if (DataProgressComponent.CanBuySomething)
            {
                MovementController.SetTarget(FinderInside.ShopTower);
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}