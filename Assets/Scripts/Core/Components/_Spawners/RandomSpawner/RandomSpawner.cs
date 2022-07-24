using Core.Environment.Island;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._Spawners.RandomSpawner
{
    public class RandomSpawner : Spawner
    {
        [Expandable] [SerializeField] private RandomData _randomData;
        [SerializeField] private FreeIsland _freeIsland;
        
        #region Enable/Disable
        private void OnEnable()
        {
            _freeIsland.OnDelightIsland += SpawnRandom;
        }

        private void OnDisable()
        {
            _freeIsland.OnDelightIsland -= SpawnRandom;

        }
        #endregion
        [Button]
        private void SpawnRandom()
        {
            Spawn(_randomData);
        }
    }
}