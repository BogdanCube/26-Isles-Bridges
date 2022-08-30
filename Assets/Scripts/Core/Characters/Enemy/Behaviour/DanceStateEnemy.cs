namespace Core.Characters.Enemy.Behaviour
{
    public class DanceStateEnemy : StateEnemy
    {
        public override void Start()
        {
            AnimationStateController.Dance();
        }
    }
}