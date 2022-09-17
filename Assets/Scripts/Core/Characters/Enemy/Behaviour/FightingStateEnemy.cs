namespace Core.Characters.Enemy.Behaviour
{
    public class FightingStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsFighting = true;
        }

        public override void Update()
        {
            if (HealthComponent.IsDeath)
            {
                BehaviourSystem.SetState(CreateInstance<DeathStateEnemy>());
            }
            else
            {
                if (Enemy.DetectorFighting.IsFight == false)
                {
                    BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
                }
            }
        }

        public override void End()
        {          
            AnimationStateController.IsFighting = false;
        }
    }
}