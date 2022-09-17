using Core.Environment.Island;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._Spawners.RandomSpawner
{
    public class RandomSpawner : Spawner
    {
        [SerializeField] private FreeIsland _freeIsland;
        [Expandable] [SerializeField] private RandomData _randomData;
        
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
        public void SpawnRandom()
        {
            Spawn(_randomData,_freeIsland);
        }
    }
}