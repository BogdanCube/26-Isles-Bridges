using Core.Character.Behavior;
using Core.Characters.Base.Behavior;
using UnityEngine;

namespace Core.Characters.Archer
{
    public class BehaviourArcher : BehaviourSystem
    {
        private void Start()
        {
            SetState(ScriptableObject.CreateInstance<IdleState>());
        }

        private void Update()
        {
            if (_character.DetectorFighting.IsFight)
            {
                SetState(ScriptableObject.CreateInstance<FightingState>());
            }
            else
            {
                SetState(ScriptableObject.CreateInstance<IdleState>());
            }
        }
    }
}