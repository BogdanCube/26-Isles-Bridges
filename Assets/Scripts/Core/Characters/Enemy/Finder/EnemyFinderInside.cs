using Core.Components._ProgressComponents.Bag;
using Core.Components._Spawners;
using Core.Environment.Block;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using Core.Environment.Tower.NoBuilding;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Characters.Enemy.Finder
{
    public class EnemyFinderInside : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        private FreeIsland _island;
        private NoBuilding _noBuilding;
        public bool IsFree => _island != null && _island.IsDelight == false;
        public FreeIsland Island => _island;
        public NoBuilding NoBuilding => _noBuilding;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FreeIsland island))
            {
                _island = island;
            }
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                _noBuilding = noBuilding;
                var shopDataTowers = _noBuilding.DisplayData.ShopDataTowers;
                var randomItem = shopDataTowers.RandomItem();
                randomItem.BuyFree();
                _bag.Reset();
            }
        }
    }
}