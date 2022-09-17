namespace Core.Characters.Recruit.Behavior
{
    public class RunningStateRecruit : StateRecruit
    {
        protected override void LateUpdate()
        {
            if (MovementController.IsMove)
            {
                MovementController.MoveToPosition();
            }
            else
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateRecruit>());
            }
        }
    }
}