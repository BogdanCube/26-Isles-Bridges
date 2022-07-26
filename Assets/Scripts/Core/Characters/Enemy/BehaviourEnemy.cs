using Core.Character.Behavior;
using Core.Components._ProgressComponents.Bag;
using Core.Components.Behavior;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class BehaviourEnemy : BehaviourSystem
    {
        private void Start()
        {
            SetState(ScriptableObject.CreateInstance<RunningState>());
        }

        private void Update()
        {
            if (_character.HealthComponent.IsDeath == false)
            {
                _currentState.Update();
                
                if (_character.DetectorFighting.IsFight)
                {
                    SetState(ScriptableObject.CreateInstance<FightingState>());
                }
                else if (_character.MovementController.IsMove)
                {
                    SetState(ScriptableObject.CreateInstance<RunningState>());
                }
                else
                {
                    SetState(ScriptableObject.CreateInstance<IdleState>());
                }
            }
            else
            {
                SetState(ScriptableObject.CreateInstance<DeathState>());
            }
        }
    }
}