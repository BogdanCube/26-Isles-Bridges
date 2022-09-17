using Core.Characters.Player.Behavior;

namespace Core.Characters.Enemy.Behaviour
{
    public class AggressiveStateEnemy : StateEnemy
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
                        if (IsTowerAttack)
                        {
                            MovementController.SetTarget(FinderTower.PlayerTower);
                        }
                        else if (FinderPlayer.IsPlayer)
                        {
                            MovementController.SetTarget(FinderPlayer.Player);
                        }
                        else if (IsRush)
                        {
                            MovementController.SetTarget(FinderPlayer.FindPlayer);
                        }
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