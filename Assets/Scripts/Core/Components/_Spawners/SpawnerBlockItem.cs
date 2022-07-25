using System.Collections;
using Core.Components._Spawners.RandomSpawner;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._Spawners
{
    public class SpawnerBlockItem : Spawner
    {
        [Expandable][SerializeField] private RandomData _randomData;
        private IEnumerator _coroutine;
        public void StartSpawn(int time)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = SpawnCoroutine(_randomData,time);
            StartCoroutine(_coroutine);
        }
    }
}