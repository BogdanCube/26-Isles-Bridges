using System.Runtime.InteropServices;
using Core.Components._ProgressComponents.Health;
using Core.Components._Spawners;
using Core.Components._Spawners.RandomSpawner;
using Core.Environment.Island;
using NaughtyAttributes;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using UnityEngine;
using UnityEngine.Scripting;

namespace Core.Components.Loot
{
    public class LootSpawner : Spawner
    {
        [Expandable][SerializeField] private RandomData _randomData;
        [InterfaceType(typeof(IHealthComponent))]
        public Object _iHealthComponent;
        private Vector2 _radius = new(0,1.16f);

        private IHealthComponent HealthComponent => _iHealthComponent as IHealthComponent;
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
            Spawn(_randomData,_radius);
        }
        public void DespawnLoot()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Deactivate();
            }
        }
    }
}