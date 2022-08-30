using Core.Character.Behavior;
using Core.Characters.Base.Behavior;

namespace Core.Characters.Player.Behavior
{
    public class IdleState : State
    {
        private Player Player => BehaviourSystem.Player;

        public override void Start()
        {
            BehaviourSystem.Player.AnimationStateController.IsRunning = false;
        }
        public override void Update()
        {
            if (Player.HealthComponent.IsDeath == false)
            {
                if (Player.MovementController.IsMove)
                {
                    BehaviourSystem.SetState(CreateInstance<RunningState>());
                }
                if (Player.DetectorFighting.IsFight)
                {
                    BehaviourSystem.SetState(CreateInstance<FightingState>());
                } 
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<DeathState>()); 
            }
        }
    }
}