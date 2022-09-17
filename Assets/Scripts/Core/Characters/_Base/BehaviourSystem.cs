using System;
using NaughtyAttributes;
using NTC.Global.Cache;
using UnityEngine;

namespace Core.Characters._Base
{
    public abstract class BehaviourSystem : NightCache,INightRun
    {
        public bool IsStop { private get; set; }
        protected abstract IState CurrentState { get; }

        private void Awake()
        {
            SetIdleState();
        }
        public void Run()
        {
            if (IsStop == false)
            {
                CurrentState.Update();
            }
        }
        
        public abstract void SetIdleState();
        public virtual void SetDanceState(){}
        public virtual void SetCryingState(){}
    }
}