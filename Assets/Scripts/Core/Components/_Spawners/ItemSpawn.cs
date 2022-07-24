using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components._Spawners
{
    public class ItemSpawn : MonoBehaviour
    {
        private Spawner _spawner;
        public void SetSpawner(Spawner spawner,Vector3 position)
        {
            _spawner = spawner;
            transform.localPosition = position;
        }
        public void SpendCount()
        {
            _spawner.Spend();
            NightPool.Despawn(this);
        }
    }
}