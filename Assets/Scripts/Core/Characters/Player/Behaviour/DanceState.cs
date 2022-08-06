using Core.Characters.Base.Behavior;

namespace Core.Characters.Player.Behaviour
{
    public class DanceState : State
    {
        public override void Start()
        {
            BehaviourSystem.Character.AnimationStateController.Dance();
        }
    }
}