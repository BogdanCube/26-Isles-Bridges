namespace Core.Characters.Enemy.Behaviour
{
    public class AheadStateEnemy : StateEnemy
    {
        public override void Update()
        {
            if (HealthComponent.IsDeath)
            {
                BehaviourSystem.SetState(CreateInstance<DeathStateEnemy>());
            }
            else
            {
                if (MovementController.IsAhead)
                {
                    MovementController.SetTargetAhead();
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