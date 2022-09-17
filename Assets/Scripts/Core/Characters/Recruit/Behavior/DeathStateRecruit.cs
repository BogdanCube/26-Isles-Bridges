namespace Core.Characters.Recruit.Behavior
{
    public class DeathStateRecruit : StateRecruit
    {
        public override void Start()
        {
            AnimationStateController.Death();
            BehaviourSystem.IsStop = true;
            MovementController.IsStopped = true;
        }
    }
}