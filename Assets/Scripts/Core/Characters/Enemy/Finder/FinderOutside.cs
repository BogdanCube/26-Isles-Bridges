using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components._Spawners;
using Core.Environment._ItemSpawn.Block;
using Core.Environment.Bridge.Brick;
using Core.Environment.Tower;
using Core.Environment.Tower._Base;
using Core.Environment.Tower.NoBuilding;
using NaughtyAttributes;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderOutside : DebugDetector
    {
        private Brick _brick;
        private BlockItem _blockItem;
        private ItemSpawn _item;
        private NoBuilding _noBuilding;
        public bool IsBrick => _brick && _brick.enabled;
        public bool IsBlockItem => _blockItem && _blockItem.IsActive();
        public bool IsItem => _item && _item.IsActive();
        public bool IsNoBuilding => _noBuilding && _noBuilding.IsActive();
        public Transform Brick => _brick.transform;
        public Transform BlockItem => _blockItem.transform;
        public Transform Item => _item.transform;
        public Transform NoBuilding => _noBuilding.transform;
        public Transform ShopTower { get; private set; }

        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out ItemSpawn item))
            {
                if (item.GetType() == typeof(BlockItem))
                {
                    _blockItem = (BlockItem)item;
                }
                else
                {
                    _item = item;
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

        private void OnTriggerEnter(Collider other)
        {
            if (ShopTower == null && other.TryGetComponent(out ShopTower shopTower))
            {
                ShopTower = shopTower.transform;
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