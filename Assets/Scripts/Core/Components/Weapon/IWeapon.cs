using System;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Components.Weapon
{
    public interface IWeapon
    {
        event Action OnTakeDamage;
        void TakeDamage(Transform target, IHealthComponent health);
    }
}