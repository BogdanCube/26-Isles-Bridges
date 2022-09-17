namespace Core.Characters.Recruit.Behavior
{
    public class FightingStateRecruit : StateRecruit
    {
        public override void Start()
        {
            AnimationStateController.IsFighting = true;
        }

        protected override void LateUpdate()
        {
            if (DetectorFighting.IsFight == false)
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateRecruit>());
            }
        }

        public override void End()
        {          
            AnimationStateController.IsFighting = false;
        }
    }
}