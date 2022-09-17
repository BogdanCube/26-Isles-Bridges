namespace Core.Characters.Archer.Behaviour
{
    public class IdleStateArcher : StateArcher
    {
        public override void Start()
        {
            AnimationStateController.IsFighting = false;
        }
        public override void Update()
        {
            if (DetectorFighting.IsFight)
            {
                BehaviourSystem.SetState(CreateInstance<FightingStateArcher>());
            }
        }
    }
}