using Core.Character.Behavior;
using Core.Components._ProgressComponents.Bag;
using Core.Components.Behavior;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class BehaviourEnemy : BehaviourSystem
    {
        [SerializeField] private Bag _bag;

        private void Start()
        {
            SetState(ScriptableObject.CreateInstance<IdleState>());
        }

        private void Update()
        {
            if (_character.HealthComponent.IsDeath == false)
            {
                _currentState.Update();
                if (_bag.CheckCount)
                {
                }
                else
                {
                    SetState(ScriptableObject.CreateInstance<DeathState>());
                }
            }
            else
            {
                SetState(ScriptableObject.CreateInstance<DeathState>());
            }
        }
    }
}