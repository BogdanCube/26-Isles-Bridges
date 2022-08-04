using System.Collections;
using Core.Components._Spawners.RandomSpawner;
using Core.Environment.Island;
using Core.Environment.Tower;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._Spawners
{
    public class SpawnerTower : Spawner
    {
        [SerializeField] private Tower _tower;
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
            _coroutine = SpawnCoroutine(_randomData,_tower.Island,_currentTimeSpawn);
            StartCoroutine(_coroutine);
        }
    }
}