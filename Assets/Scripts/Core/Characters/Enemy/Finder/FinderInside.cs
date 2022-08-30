using System;
using System.Linq;
using Core.Components._ProgressComponents.Bag;
using Core.Components._Spawners;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using Core.Environment.Block;
using Core.Environment.Island;
using Core.Environment.Tower;
using Core.Environment.Tower.NoBuilding;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderInside : MonoBehaviour
    {
        [SerializeField] private DataTowers _dataTowers;
        [SerializeField] private DataProgressComponent _dataProgress;
        [SerializeField] private BagCharacter _bag;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private MovementEnemy _movementEnemy;
        public Transform ShopTower { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                if (_dataTowers.CanBuySomething)
                {
                    var displayData = noBuilding.DisplayData;
                    displayData.Load(_dataTowers.TowerData, _dataTowers.Bag);
                    var shopDataTowers = displayData.ShopDataTowers;
                    
                    foreach (var data in shopDataTowers.Where(data => _bag.HasCanSpend(data.Price)))
                    {
                        data.BuyAhead(_movementEnemy.SetAhead); 
                        _bag.Spend(data.Price);
                        displayData.Deload();
                        break;
                    }
                }
            }
            if (other.TryGetComponent(out Tower tower) && other.TryGetComponent(out ShopTower shopTower))
            {
                if (tower.Owner == _bag.Character)
                {
                    ShopTower = tower.transform;
                    var dataProgress = _dataProgress.Сomponents;
                    
                    if (dataProgress.Count(data => data.Price <= _wallet.CurrentCount && data.IsMaxLevel == false) > 0)
                    {
                        foreach (var data in dataProgress)
                        {
                            if(_wallet.HasCanSpend(data.Price) && data.IsMaxLevel == false)
                            {
                                _wallet.Spend(data.Price);
                                data.LevelUp();
                            }
                        }
                    } 
                }
            }
        }
    }
}