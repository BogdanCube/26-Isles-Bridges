namespace Core.Characters.Enemy.Behaviour
{
    public class DeathStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.Death();
            BehaviourSystem.IsStop = true;
            MovementController.IsStopped = true;
        }
    }
}