using Core.Environment.Block;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderIsland : MonoBehaviour
    {
        private FreeIsland _island;
        public bool IsFree => _island != null && _island.IsDelight == false;
        public FreeIsland Island => _island;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FreeIsland island))
            {
                _island = island;
            }
        }
    }
}