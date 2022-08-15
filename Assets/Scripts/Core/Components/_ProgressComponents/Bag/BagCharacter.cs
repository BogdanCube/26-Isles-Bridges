using Core.Components._ProgressComponents.Health;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._ProgressComponents.Bag
{
    public class BagCharacter : Bag
    {
        [SerializeField] private Characters.Base.Character _character;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private float _chanceHeal = 0.1f;
        public Characters.Base.Character Character => _character;
        public override void Spend(int count = 1)
        {
            base.Spend(count);
            if (_chanceHeal > Random.value)
            {
                _healthComponent.Heal();
            }
        }
    }
}