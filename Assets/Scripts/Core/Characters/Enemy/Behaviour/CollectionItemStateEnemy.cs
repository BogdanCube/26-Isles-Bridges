namespace Core.Characters.Enemy.Behaviour
{
    public class CollectionItemStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }
        public override void UpdateAction()
        {
            if (FinderOutside.IsItem)
            {
                MovementController.SetCheckTarget(FinderOutside.Item);
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}