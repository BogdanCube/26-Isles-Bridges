using Core.Character.Behavior;
using Core.Characters.Base.Behavior;

namespace Core.Characters.Player.Behavior
{
    public class FightingState : State
    {       
        private Characters.Player.Player Player => BehaviourSystem.Player;

        public override void Start()
        {
            BehaviourSystem.Player.AnimationStateController.IsFighting = true;
        }

        public override void Update()
        {
            if (Player.HealthComponent.IsDeath == false)
            {
                if (Player.MovementController.IsMove || Player.DetectorFighting.IsFight == false)
                {
                    BehaviourSystem.SetState(CreateInstance<RunningState>());
                }
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<DeathState>()); 
            }
        }

        public override void End()
        {          
            BehaviourSystem.Player.AnimationStateController.IsFighting = false;
        }
    }
}
