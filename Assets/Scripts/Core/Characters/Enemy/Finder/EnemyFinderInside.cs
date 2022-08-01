using System.Linq;
using Core.Components._ProgressComponents.Bag;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using Core.Environment.Island;
using Core.Environment.Tower;
using Core.Environment.Tower.NoBuilding;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class EnemyFinderInside : MonoBehaviour
    {
        [SerializeField] private BagCharacter _bag;
        [SerializeField] private Wallet _wallet;
        private FreeIsland _island;
        private NoBuilding _noBuilding;
        private Tower _tower;
        public bool IsFree => _island != null && _island.IsDelight == false;
        public FreeIsland Island => _island;
        public NoBuilding NoBuilding => _noBuilding;
        public Tower ShopTower => _tower;

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
                if (_bag.HasCanSpend(randomItem.Price))
                {
                    randomItem.BuyAhead();
                    _bag.Reset();
                }
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner == _bag.Character)
                {
                    _tower = tower;
                    var shop = _tower.ShopTower;
                    var dataProgress =shop.DisplayProgress.ShopDataTowers;
                    
                    if (dataProgress.Count(data => data.Price <= _wallet.CurrentCount) > 0)
                    {
                        foreach (var data in dataProgress)
                        {
                            if(_wallet.HasCanSpend(data.Price))
                            {
                                data.BuyAhead();
                            }
                        }
                    }
                }
            }

        }
    }
}