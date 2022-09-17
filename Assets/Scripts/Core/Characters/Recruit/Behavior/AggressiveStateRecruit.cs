namespace Core.Characters.Recruit.Behavior
{
    public class AggressiveStateRecruit : StateRecruit
    {
        public override void Update()
        {
            if (HealthComponent.IsDeath)
            {
                BehaviourSystem.SetState(CreateInstance<DeathStateRecruit>());
            }
            else
            {
                if (DetectorFighting.IsFight)
                {
                    BehaviourSystem.SetState(CreateInstance<FightingStateRecruit>());
                }
                else
                {
                    if (IsAggresive)
                    {
                        MovementController.SetTarget(DetectorCharacter.CurrentTarget);
                        MovementController.MoveToTarget();
                    }
                    else
                    {
                        BehaviourSystem.SetState(CreateInstance<IdleStateRecruit>());
                    }
                }
            }
        }
    }
}