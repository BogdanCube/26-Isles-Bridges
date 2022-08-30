using Core.Characters._Base;
using UnityEngine;

namespace Core.Characters.Archer
{
    public class BehaviourArcher : BehaviourSystem
    {
        protected override IState CurrentState { get; }

        protected override void SetIdleState()
        {
        }
    }
}