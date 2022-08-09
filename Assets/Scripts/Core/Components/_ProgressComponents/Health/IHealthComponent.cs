using System;

namespace Core.Components._ProgressComponents.Health
{
    public interface IHealthComponent
    {
        bool IsDeath { get; }
        Action OnDeath { get; set; }
        Action OnOver { get; set; }
        void Hit(int damage);
    }
}