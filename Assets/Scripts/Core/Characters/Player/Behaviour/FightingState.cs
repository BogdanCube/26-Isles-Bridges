namespace Core.Character.Behavior
{
    public class FightingState : State
    {
        public override void Start()
        {
            BehaviourSystem.Character.AnimationStateController.IsFighting = true;
        }

        public override void Update()
        {
            
        }

        public override void End()
        {          
            BehaviourSystem.Character.AnimationStateController.IsFighting = false;
        }
    }
}
