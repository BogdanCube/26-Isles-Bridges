using System;

namespace Core.Components._ProgressComponents.Health
{
    public interface IHealthComponent
    {
        Action OnDeath { get; set; }
        bool IsDeath { get; set; }
        void Hit(int damage);
    }
}