namespace Core.Characters.Enemy.Behaviour
{
    public class CollectionBrickStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }
        public override void UpdateAction()
        {
            if (Enemy.IsCollection)
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