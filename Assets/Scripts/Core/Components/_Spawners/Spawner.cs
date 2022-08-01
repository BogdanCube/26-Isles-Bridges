using System;
using System.Collections;
using Core.Components._ProgressComponents;
using Core.Components._Spawners.RandomSpawner;
using Core.Environment.Block;
using NaughtyAttributes;
using NTC.Global.Pool;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._Spawners
{
    public class Spawner : MonoBehaviour
    {
        [MinMaxSlider(0f, 10f)] [SerializeField]
        private Vector2 _radius;
        [SerializeField] private int _limitSpawn;

        [Space] [SerializeField] private int _currentCount;
        [SerializeField] private float _coeffienceRadious = 1.5f;
        private float RandomMinus => Random.Range(-1f, 1f) > 0 ? 1 : - 1;
        private float RandomPos => RandomMinus * _radius.RandomRange();
        
        protected IEnumerator SpawnCoroutine(RandomData randomData,int timeSpawn)
        {
            while (true)
            {
                if (_currentCount < _limitSpawn)
                {
                    Spawn(randomData);
                }
                yield return new WaitForSeconds(timeSpawn);
            }
        }
        protected void Spawn(RandomData randomData)
        {
            var currentTemplate = randomData.Templates.RandomItem();
            var count = currentTemplate.CountSpawn.RandomRange();
            
            for (int i = 0; i < count; i++)
            {
                _currentCount++;
                var itemSpawn = NightPool.Spawn(currentTemplate.ItemSpawn, transform);
                var randomVector = new Vector3(RandomPos, 0, RandomPos);
                itemSpawn.SetSpawner(this,randomVector);
            }
        }
        public void Spend()
        {
            _currentCount--;
        }

        #region Debug
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius.x * _coeffienceRadious);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius.y * _coeffienceRadious);
            
        }
        #endregion
    }
}