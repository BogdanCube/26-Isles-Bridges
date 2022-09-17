namespace Core.Characters.Enemy.Behaviour
{
    public class CollectionBrickStateEnemy : StateEnemy
    {
        protected override void LateUpdate()
        {
            if (IsCollection)
            {
                MovementController.SetTarget(FinderOutside.BlockItem);
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}