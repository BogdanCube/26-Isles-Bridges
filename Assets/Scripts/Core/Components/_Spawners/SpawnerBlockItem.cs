using Core.Components._Spawners;
using Core.Components._Spawners.RandomSpawner;
using Core.Environment.Block;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Environment.Tower
{
    public class SpawnerBlockItem : Spawner
    {
        [SerializeField] private LoaderTimeSpawn _loaderTimeSpawn;
        [Expandable][SerializeField] private RandomData _randomData;
        [ShowNativeProperty] private int Time => _loaderTimeSpawn.TimeSpawn;
        private void Start()
        {
            StartCoroutine(SpawnCoroutine(_randomData,Time));
        }
    }
}