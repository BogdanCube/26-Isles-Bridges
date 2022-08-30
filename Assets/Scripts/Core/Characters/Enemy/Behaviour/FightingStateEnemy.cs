namespace Core.Characters.Enemy.Behaviour
{
    public class FightingStateEnemy : StateEnemy
    {
        public override void Start()
        {
            Enemy.AnimationStateController.IsFighting = true;
        }

        public override void UpdateAction()
        {
            if (Enemy.DetectorFighting.IsFight == false)
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateEnemy>());
            }
        }

        public override void End()
        {          
            Enemy.AnimationStateController.IsFighting = false;
        }
    }
}