namespace Core.Characters.Player.Behavior
{
    public class IdleState : StatePlayer
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = false;
        }
        public override void Update()
        {
            if (HealthComponent.IsDeath == false)
            {
                if (MovementController.IsMove)
                {
                    BehaviourSystem.SetState(CreateInstance<RunningState>());
                }
                if (DetectorFighting.IsFight)
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