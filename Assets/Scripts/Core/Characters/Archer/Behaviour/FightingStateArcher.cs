namespace Core.Characters.Archer.Behaviour
{
    public class FightingStateArcher : StateArcher
    {
        public override void Start()
        {
            AnimationStateController.IsFighting = true;
        }

        public override void Update()
        {
            if (DetectorFighting.IsFight == false)
            {
                BehaviourSystem.SetState(CreateInstance<IdleStateArcher>());
            }
        }

        public override void End()
        {
            AnimationStateController.IsFighting = false;
        }
    }
}