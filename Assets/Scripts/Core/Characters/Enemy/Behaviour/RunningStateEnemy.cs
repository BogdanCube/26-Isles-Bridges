namespace Core.Characters.Enemy.Behaviour
{
    public class RunningStateEnemy : StateEnemy
    {
        public override void Start()
        {
            MovementController.SpeedBoost(1.5f);
            AnimationStateController.IsRunning = true;
        }
        public override void UpdateAction()
        {
            if (Enemy.IsNonState)
            {
                MovementController.SetStartTarget();
                MovementController.Move();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>()); 
            }
        }

        public override void End()
        {
            MovementController.SpeedDeboost();
        }
    }
    
}