using Core.Characters.Player.Behavior;

namespace Core.Characters.Enemy.Behaviour
{
    public class AggressiveStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }
        public override void UpdateAction()
        {
            AnimationStateController.IsRunning = false;
            if (Enemy.IsAggressive)
            {
                /*if (FinderOutside.PlayerTower)
                {
                    MovementController.SetCheckTarget(FinderOutside.PlayerTower);
                }*/
                
                MovementController.SetStartTarget();
                /*if (FinderPlayer.IsPlayer)
                {
                    MovementController.SetTarget(FinderPlayer.Player);
                }
                MovementController.Move();*/
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }
    }
}