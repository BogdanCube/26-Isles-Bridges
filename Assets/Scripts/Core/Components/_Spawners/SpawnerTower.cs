using System.Collections;
using Core.Components._Spawners.RandomSpawner;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._Spawners
{
    public class SpawnerTower : Spawner
    {
        [Expandable][SerializeField] private RandomData _randomData;
        [SerializeField] private int _currentTimeSpawn;
        private IEnumerator _coroutine;
        
        public void StartSpawn(int time)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _currentTimeSpawn = time;
            _coroutine = SpawnCoroutine(_randomData,_currentTimeSpawn);
            StartCoroutine(_coroutine);
        }
    }
}