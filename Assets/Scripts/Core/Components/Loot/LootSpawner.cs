using Core.Components._ProgressComponents.Health;
using Core.Components._Spawners;
using Core.Components._Spawners.RandomSpawner;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components.Loot
{
    public class LootSpawner : Spawner
    {
        [Expandable][SerializeField] private RandomData _randomData;
        [SerializeField] private Object _healthComponent;
        private IHealthComponent HealthComponent => _healthComponent as IHealthComponent;
        #region Enable/Disable
        private void OnEnable()
        {
            HealthComponent.OnDeath += SpawnLoot;
        }

        private void OnDisable()
        {
            HealthComponent.OnDeath -= SpawnLoot;
        }
        #endregion

        private void SpawnLoot()
        {
            Spawn(_randomData);
        }
    }
}