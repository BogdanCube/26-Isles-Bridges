using System;
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
        [Expandable][SerializeField] private RandomData _randomData;
        [ShowNonSerializedField] private int _currentTimeSpawn;
        private IEnumerator _coroutine;
        
        public void StartSpawn(int time, Tower tower, Action<int> callback)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _currentTimeSpawn = time;
            _coroutine = SpawnCoroutine(_randomData,tower.Island,_currentTimeSpawn,callback);
            StartCoroutine(_coroutine);
        }

        public void ResetSpawn(Action<Action> callback)
        {
            StopCoroutine(_coroutine);
            callback?.Invoke(() =>
            {
                StartCoroutine(_coroutine);
            });
        }
    }
}