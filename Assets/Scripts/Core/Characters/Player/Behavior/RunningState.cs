using Core.Character.Behavior;
using Core.Characters.Base.Behavior;

namespace Core.Characters.Player.Behavior
{
    public class RunningState : State
    {
        private Player Player => BehaviourSystem.Player;

        public override void Start()
        {
            Player.AnimationStateController.IsRunning = true;
        }
        public override void Update()
        {
            if (Player.HealthComponent.IsDeath == false)
            {
                if (Player.DetectorFighting.IsFight)
                {
                    BehaviourSystem.SetState(CreateInstance<FightingState>());
                }
                if (Player.MovementController.IsMove)
                {
                    Player.MovementController.Move();
                }
                else
                {
                    BehaviourSystem.SetState(CreateInstance<IdleState>());
                }
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<DeathState>()); 
            }
        }

        public override void End()
        {
            Player.AnimationStateController.IsRunning = false;
        }
    }
}
