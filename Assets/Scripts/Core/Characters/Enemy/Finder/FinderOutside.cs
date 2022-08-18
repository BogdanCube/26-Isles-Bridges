using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components._ProgressComponents.Health;
using Core.Components._Spawners;
using Core.Environment._ItemSpawn;
using Core.Environment._ItemSpawn.Block;
using Core.Environment.Block;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using Core.Environment.Tower;
using Core.Environment.Tower.NoBuilding;
using NaughtyAttributes;
using Rhodos.Toolkit.Extensions;
using UnityEditor;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderOutside : DebugDetector
    {
        private Brick _brick;
        private Tower _tower;
        public bool IsBrick => _brick &&_brick.enabled && _brick.IsSet == false;
        public bool IsTower => _tower && _tower.Level.IsMaxLevel == false;
        public bool IsTarget => PlayerTower || Item;

        public Transform Brick => _brick.transform;
        public Transform Tower => _tower.DetectorBag.transform;
        public Transform PlayerTower { get; private set; }
        public Transform Item { get; private set; }
        public Transform NoBuilding { get; private set; }
        public Transform BlockItem { get; private set; }

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
            }
            
            if (other.TryGetComponent(out ItemSpawn item))
            {
                if (item.GetType() != typeof(BlockItem))
                {
                    Item = item.transform;
                }
                else
                {
                    BlockItem = item.transform;
                }
            }
            
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    _brick = brick;
                }
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy) && tower.HealthComponent.IsDeath == false)
                {
                    if (tower.Level.IsMaxLevel == false)
                    {
                        _tower = tower;
                    }
                }
            }
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                NoBuilding = noBuilding.transform;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ItemSpawn item))
            {
                Item = null;
            }
            if (other.TryGetComponent(out BlockItem blockItem))
            {
                BlockItem = null;
            }
        }
    }
}