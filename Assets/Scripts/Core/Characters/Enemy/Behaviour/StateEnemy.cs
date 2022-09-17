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
        protected FinderTower FinderTower => Enemy.FinderTower;
        protected DataTowers DataTowers => Enemy.DataTowers;
        protected DataProgressComponent DataProgressComponent => Enemy.DataProgress;

        #region Bool

        protected bool IsAggressive => 
            IsTowerAttack 
            || (FinderPlayer.IsPlayer && MovementController.CheckTarget(FinderPlayer.Player)) 
            || IsRush;

        protected bool IsRush => FinderTower.IsFullMax && IsBuildTower == false; 
        //protected bool IsTowerAttack => FinderTower.IsTowerPlayer;
        protected bool IsTowerAttack => FinderTower.IsTowerPlayer && FinderTower.IsFullMax;
        private bool HasBrick => Enemy.Bag.CheckCount(0.5f);
        protected bool IsCollection => FinderOutside.IsBlockItem && Enemy.Bag.HasCanAdd;
        protected bool IsCollectionItem => FinderOutside.IsItem && MovementController.CheckTarget(FinderOutside.Item);
        protected bool IsNonState => (HasBrick == false || IsCollection == false) && MovementController.IsAtStart == false;
        protected bool IsBuildTower => HasBrick && FinderTower.IsTower;
        protected bool IsBuild => HasBrick && (FinderOutside.IsBrick || (FinderOutside.IsNoBuilding && DataTowers.CanBuySomething));
        #endregion
      

        public virtual void Start()
        {
            AnimationStateController.IsRunning = true;
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
                    if (IsAggressive)
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
                            LateUpdate();
                        }
                    }
                }
            }
        }
        public virtual void End() { }
        
        protected virtual void LateUpdate() {}

    }
}