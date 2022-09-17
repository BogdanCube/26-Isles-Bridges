namespace Core.Characters.Recruit.Behavior
{
    public class IdleStateRecruit : StateRecruit
    {
        public override void Start()
        {
            AnimationStateController.IsRunning = false;
        }
        protected override void LateUpdate()
        {
            if (MovementController.IsMove)
            {
                BehaviourSystem.SetState(CreateInstance<RunningStateRecruit>());
            }
        }
    }
}