using System;
using UnityEngine;

namespace Core.Components._ProgressComponents.Health
{
    public interface IHealthComponent
    {
        bool IsDeath { get; }
        event Action<Transform> OnHit;
        event Action OnDeath;
        event Action OnOver;
        void Hit(int damage);
        void Over();
    }
}