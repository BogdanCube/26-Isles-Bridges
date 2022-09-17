namespace Core.Characters.Enemy.Behaviour
{
    public class BuyProgressStateEnemy : StateEnemy
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
                        if (DataProgressComponent.CanBuySomething)
                        {
                            MovementController.SetTarget(FinderOutside.ShopTower);
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