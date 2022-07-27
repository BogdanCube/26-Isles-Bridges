using Core.Characters.Base.Behavior;

namespace Core.Character.Behavior
{
    public class RunningState : State
    {
        public override void Start()
        {
            BehaviourSystem.Character.AnimationStateController.IsRunning = true;
        }
        public override void Update()
        {
            BehaviourSystem.Character.MovementController.Move();
        }

        public override void End()
        {
            BehaviourSystem.Character.AnimationStateController.IsRunning = false;
        }
    }
}
