using Core.Environment.Tower;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment.Block
{
    public class BlockItem : MonoBehaviour
    {
        private SpawnerBlockItem _spawner;
        public void SetSpawner(SpawnerBlockItem spawner)
        {
            _spawner = spawner;
        }
        public void SpendCount()
        {
            _spawner.Spend();
            NightPool.Despawn(this);
        }
    }
}