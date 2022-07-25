using System;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._Spawners
{
    public class ItemSpawn : MonoBehaviour
    {
        [MinMaxSlider(0, 360)] [SerializeField] private Vector2 _rotationY;
        private Spawner _spawner;
        public void SetSpawner(Spawner spawner,Vector3 position)
        {
            _spawner = spawner;
            transform.localPosition = position;
            //SetRandomAngle();
        }
        public void SpendCount()
        {
            _spawner.Spend();
            NightPool.Despawn(this);
        }

        protected void SetRandomAngle()
        {
            var randomRotation = Random.Range(_rotationY.x, _rotationY.y);
            transform.localRotation = Quaternion.Euler(0, randomRotation, 0);
        }
    }
}