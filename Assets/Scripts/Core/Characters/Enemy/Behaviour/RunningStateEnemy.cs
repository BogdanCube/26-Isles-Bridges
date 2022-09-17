namespace Core.Characters.Enemy.Behaviour
{
    public class RunningStateEnemy : StateEnemy
    {
        protected override void LateUpdate()
        {
            if (DataProgressComponent.CanBuySomething)
            {
                BehaviourSystem.SetState(CreateInstance<BuyProgressStateEnemy>());
            }
            else
            {
                if (IsCollectionItem)
                {
                    BehaviourSystem.SetState(CreateInstance<CollectionItemStateEnemy>());
                }
                else
                {
                    if (IsNonState)
                    {
                        MovementController.SetStartTarget();
                        MovementController.Move();
                    }
                    else
                    {
                        BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>()); 
                    }
                }
            }
        }
    }
}