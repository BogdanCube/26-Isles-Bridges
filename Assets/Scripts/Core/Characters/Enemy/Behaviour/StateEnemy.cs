using System;
using Core.Characters._Base;
using Core.Characters.Enemy.Finder;
using Core.Characters.Player;
using Core.Characters.Player.Behavior;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Components.DataTowers;
using UnityEngine;

namespace Core.Characters.Enemy.Behaviour
{
    public abstract class StateEnemy : ScriptableObject,IState
    {
        public BehaviourEnemy BehaviourSystem;
        protected Enemy Enemy => BehaviourSystem.Enemy;
        protected IHealthComponent HealthComponent => Enemy.HealthComponent;
        protected MovementEnemy MovementController => Enemy.MovementController;
        protected AnimationStateController AnimationStateController => Enemy.AnimationStateController;
        protected FinderOutside FinderOutside => Enemy.FinderOutside;
        protected FinderInside FinderInside => Enemy.FinderInside;
        protected FinderPlayer FinderPlayer => Enemy.FinderPlayer;
        protected DataTowers DataTowers => Enemy.DataTowers;
        protected DataProgressComponent DataProgressComponent => Enemy.DataProgress;

        public virtual void Start()
        {
            Enemy.AnimationStateController.IsRunning = true;
        }

        public virtual void Update()
        {
            if (HealthComponent.IsDeath)
            {
                BehaviourSystem.SetState(CreateInstance<DeathStateEnemy>());
            }
            else
            {
                if (Enemy.DetectorFighting.IsFight)
                {
                    BehaviourSystem.SetState(CreateInstance<FightingStateEnemy>());
                }
                else
                {
                    if (Enemy.IsAggressive)
                    {
                        BehaviourSystem.SetState(CreateInstance<AggressiveStateEnemy>());
                    }
                    else
                    {
                        if (MovementController.IsAhead)
                        {
                            BehaviourSystem.SetState(CreateInstance<AheadStateEnemy>());
                        }
                        else
                        {
                            if (DataProgressComponent.CanBuySomething)
                            {
                                BehaviourSystem.SetState(CreateInstance<BuyProgressStateEnemy>());
                            }
                            else
                            {
                                UpdateAction();
                                /*if (FinderOutside.IsItem)
                                {
                                    BehaviourSystem.SetState(CreateInstance<CollectionItemStateEnemy>());
                                }
                                else
                                {
                                    UpdateAction();
                                }*/
                            }
                        }
                    }
                }
            }
        }
        public virtual void End()
        {
            Enemy.AnimationStateController.IsRunning = false;
        }
        
        
        public virtual void UpdateAction() {}

    }
}