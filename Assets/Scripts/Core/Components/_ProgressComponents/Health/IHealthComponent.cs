namespace Core.Components._ProgressComponents.Health
{
    public interface IHealthComponent
    {
        bool IsDeath { get; set; }
        void Hit(int damage);
    }
}