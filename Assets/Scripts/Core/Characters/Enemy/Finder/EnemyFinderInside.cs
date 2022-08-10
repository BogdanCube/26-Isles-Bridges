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
        public Transform NoBuilding { get; private set; }
        public Transform ShopTower { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                var shopDataTowers = noBuilding.DisplayData.ShopDataTowers;
               
                if (shopDataTowers.Count(data => data.Price <= _bag.CurrentCount) > 0)
                {
                    NoBuilding = noBuilding.transform;
                    foreach (var data in shopDataTowers)
                    {
                        if(_bag.HasCanSpend(data.Price))
                        {
                            data.BuyAhead();
                            _bag.Reset();
                            break;
                        }
                    }
                }
            }
            if (other.TryGetComponent(out Tower tower) && other.TryGetComponent(out ShopTower shopTower))
            {
                if (tower.Owner == _bag.Character)
                {
                    var dataProgress =shopTower.DisplayProgress.ShopDataTowers;
                    
                    if (dataProgress.Count(data => data.Price <= _wallet.CurrentCount) > 0)
                    {
                        ShopTower = tower.transform;
                        foreach (var data in dataProgress)
                        {
                            if(_wallet.HasCanSpend(data.Price))
                            {
                                data.BuyAhead();
                                break;
                            }
                        }
                    }
                    else
                    {
                        ShopTower = null;
                    }
                }
            }
        }
    }
}