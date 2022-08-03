using System;
using Core.Character.Behavior;
using Core.Characters.Base.Behavior;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Character.Player
{
    public class BehaviourPlayer : BehaviourSystem
    {
        private void Start()
        {
            SetState(ScriptableObject.CreateInstance<IdleState>());
        }

        private void Update()
        {
            _currentState.Update();
            if (_character.HealthComponent.IsDeath == false)
            {
                if (_character.MovementController.IsMove)
                {
                    SetState(ScriptableObject.CreateInstance<RunningState>());
                    
                }
                else if (_character.DetectorFighting.IsFight)
                {
                    SetState(ScriptableObject.CreateInstance<FightingState>());
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