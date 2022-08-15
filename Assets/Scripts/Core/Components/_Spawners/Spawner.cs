using System;
using System.Collections;
using Core.Components._ProgressComponents;
using Core.Components._Spawners.RandomSpawner;
using Core.Environment.Block;
using Core.Environment.Island;
using NaughtyAttributes;
using NTC.Global.Pool;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _limitSpawn;
        [ShowNonSerializedField] private int _currentCount;
        private float _coeffientToIsland = 3f;
        private float RandomMinus => Random.Range(-1f, 1f) > 0 ? 1 : - 1;
        
        protected IEnumerator SpawnCoroutine(RandomData randomData, Island island, int timeSpawn, Action<int> callback)
        {
            while (true)
            {
                if (_currentCount < _limitSpawn)
                {
                    callback?.Invoke(timeSpawn);
                    Spawn(randomData,island);
                }
                yield return new WaitForSeconds(timeSpawn);
            }
        }
        protected void Spawn(RandomData randomData,Island island)
        {
            var currentTemplate = randomData.Templates.RandomItem();
            var count = currentTemplate.CountSpawn.RandomRange();
            
            for (int i = 0; i < count; i++)
            {
                _currentCount++;
                var itemSpawn = NightPool.Spawn(currentTemplate.ItemSpawn, transform);
                var randomVector = new Vector3(RandomMinus * GetRandomPos(island), 0, RandomMinus * GetRandomPos(island));
                itemSpawn.SetSpawner(this,randomVector);
            }
        }

        protected void Spawn(RandomData randomData, Vector2 radius)
        {
            var currentTemplate = randomData.Templates.RandomItem();
            var count = currentTemplate.CountSpawn.RandomRange();
            
            for (int i = 0; i < count; i++)
            {
                _currentCount++;
                var itemSpawn = NightPool.Spawn(currentTemplate.ItemSpawn, transform);
                var randomVector = new Vector3(RandomMinus * radius.RandomRange(), 0, RandomMinus * radius.RandomRange());
                itemSpawn.SetSpawner(this,randomVector);
            }
        }
        
        private float GetRandomPos(Island island)
        {
            var spawnRadius = new Vector2(island.Radius * 0.3f,island.Radius);
            return (RandomMinus * spawnRadius.RandomRange()) / _coeffientToIsland;
        }
        public void Spend()
        {
            _currentCount--;
        }
    }
}