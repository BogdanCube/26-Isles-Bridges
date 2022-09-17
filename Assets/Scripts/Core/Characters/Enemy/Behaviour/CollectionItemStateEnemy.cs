namespace Core.Characters.Enemy.Behaviour
{
    public class CollectionItemStateEnemy : StateEnemy
    {
        public override void Update()
        {
            if (HealthComponent.IsDeath)
            {
                BehaviourSystem.SetState(CreateInstance<DeathStateEnemy>());
            }
            else
            {
                if (Enemy.DetectorFighting.IsFight)
                {
                    BehaviourSystem.SetState(CreateInstance<FightingStateEnemy>());
                }
                else
                {
                    if (IsAggressive)
                    {
                        BehaviourSystem.SetState(CreateInstance<AggressiveStateEnemy>());
                    }
                    else
                    {
                        if (IsCollectionItem)
                        {
                            MovementController.SetTarget(FinderOutside.Item);
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
}