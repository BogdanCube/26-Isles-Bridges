namespace Core.Characters.Player.Behavior
{
    public class FightingState : StatePlayer
    {
        public override void Start()
        {
            AnimationStateController.IsFighting = true;
        }

        public override void Update()
        {
            if (HealthComponent.IsDeath == false)
            {
                if (MovementController.IsMove || DetectorFighting.IsFight == false)
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
            AnimationStateController.IsFighting = false;
        }
    }
}
