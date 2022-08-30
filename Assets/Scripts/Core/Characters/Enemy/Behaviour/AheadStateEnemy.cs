namespace Core.Characters.Enemy.Behaviour
{
    public class AheadStateEnemy : StateEnemy
    {
        public override void Start()
        {
            MovementController.SpeedBoost(2.5f);
            AnimationStateController.IsRunning = true;
        }
        public override void UpdateAction()
        {
            if (Enemy.IsAggressive)
            {
                MovementController.Move();
                MovementController.SetTargetAhead();
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