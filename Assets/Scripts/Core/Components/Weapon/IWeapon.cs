using System;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Components.Weapon
{
    public interface IWeapon
    {
        Action OnTakeDamage { get; set; }
        void TakeDamage(Transform target, IHealthComponent health);
    }
}