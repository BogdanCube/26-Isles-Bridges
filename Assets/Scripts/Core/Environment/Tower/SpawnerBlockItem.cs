using System;
using System.Collections;
using Core.Environment.Block;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Environment.Tower
{
    public class SpawnerBlockItem : MonoBehaviour
    {
        [SerializeField] private BlockItem _block;

        [MinMaxSlider(0f, 10f)] [SerializeField]
        private Vector2 _radius;
        
        [MinMaxSlider(0, 10)] [SerializeField]
        private Vector2Int _countSpawn;
        [SerializeField] private int _limitCount;
        [SerializeField] private float _timeSpawn;
        [SerializeField] private float _heightSpawn;

        [Space] [SerializeField] private int _currentCount;
        [SerializeField] private float _coeffienceRadious;

        private float RandomMinus => Random.Range(-1f, 1f) > 0 ? 1 : - 1;
        private float RandomPos => RandomMinus * Random.Range(_radius.x, _radius.y);
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

       
        private IEnumerator SpawnCoroutine()
        {
            while (_currentCount < _limitCount)
            {
                var count = Random.Range(_countSpawn.x, _countSpawn.y);
                for (int i = 0; i < count; i++)
                {
                    Spawn();
                }
                yield return new WaitForSeconds(_timeSpawn);
            }
        }
        [Button]
        private void Spawn()
        {
            _currentCount++;
            var randomVector = new Vector3(RandomPos, _heightSpawn, RandomPos);
            var block = NightPool.Spawn(_block, randomVector);
            block.SetSpawner(this);
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