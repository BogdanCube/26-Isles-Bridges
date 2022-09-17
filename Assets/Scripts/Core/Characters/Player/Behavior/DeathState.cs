namespace Core.Characters.Player.Behavior
{
    public class DeathState : StatePlayer
    {
        public override void Start()
        {
            AnimationStateController.Death();
            BehaviourSystem.IsStop = true;
            MovementController.IsStopped = true;
        }
    }
}