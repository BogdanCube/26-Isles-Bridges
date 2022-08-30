using Core.Components._Spawners;
using Core.Environment._ItemSpawn.Block;
using Core.Environment.Bridge.Brick;
using Core.Environment.Tower;
using Core.Environment.Tower.NoBuilding;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderOutside : DebugDetector
    {
        private Brick _brick;
        private Tower _tower;
        private BlockItem _blockItem;
        private ItemSpawn _item;
        private NoBuilding _noBuilding;
        public bool IsBrick => _brick && _brick.enabled;
        public bool IsTower => _tower && _tower.Level.IsMaxLevel == false;
        public bool IsBlockItem => _blockItem && _blockItem.gameObject.activeSelf;
        public bool IsItem => _item && _item.gameObject.activeSelf;
        public bool IsNoBuilding => _noBuilding && _noBuilding.gameObject.activeSelf;
        public Transform Brick => _brick.transform; 
        public Transform Tower => _tower.BaseDetectorBag;
        public Transform BlockItem => _blockItem.transform;
        public Transform Item => _item.transform;
        public Transform NoBuilding => _noBuilding.transform;
        public Transform PlayerTower { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Player.Player))
                {
                    if (tower.HealthComponent.IsDeath == false)
                    {
                        PlayerTower = tower.transform;
                    }
                }
                else if (tower.Owner.GetType() == typeof(Enemy) && tower.HealthComponent.IsDeath == false)
                {
                    if (tower.Level.IsMaxLevel == false)
                    {
                        _tower = tower;
                    }
                }
            }
            
            if (other.TryGetComponent(out ItemSpawn item))
            {
                if (item.GetType() != typeof(BlockItem))
                {
                    _item = item;
                }
                else
                {
                    _blockItem = (BlockItem)item;
                }
            }
            if (other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    _brick = brick;
                }
            }
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                _noBuilding = noBuilding;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ItemSpawn item))
            {
                _item = null;
            }
            if (other.TryGetComponent(out BlockItem blockItem))
            {
                _blockItem = null;
            }
        }
    }
}