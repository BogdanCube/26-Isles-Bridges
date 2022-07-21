using Core.Environment.Bridge.Brick;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment.Island
{
    public class DespawnBrick : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Brick brick))
            {
                NightPool.Despawn(brick);
            }
        }
    }
}