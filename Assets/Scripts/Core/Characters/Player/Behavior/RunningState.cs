namespace Core.Characters.Player.Behavior
{
    public class RunningState : StatePlayer
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = true;
        }
        public override void Update()
        {
            if (HealthComponent.IsDeath == false)
            {
                if (MovementController.IsMove)
                {
                    MovementController.Move();
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
            AnimationStateController.IsRunning = false;
        }
    }
}
