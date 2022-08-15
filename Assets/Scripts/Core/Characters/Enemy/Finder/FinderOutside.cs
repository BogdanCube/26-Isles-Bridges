using System;
using System.Collections.Generic;
using System.Linq;
using Core.Components._ProgressComponents.Health;
using Core.Components._Spawners;
using Core.Environment._ItemSpawn;
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
        private TowerLevel _tower;
        private Player.Player _player;
        public bool IsBrick => _brick && _brick.IsSet == false;
        public bool IsTower => _tower && _tower.IsMaxLevel == false;
        public bool IsTarget => PlayerTower || IsPlayer || Item;
        public bool IsPlayer =>_player && _player.HealthComponent.IsDeath == false;

        public Transform Brick => _brick.transform;
        public Transform Tower => _tower.transform;
        public Transform Player => _player.transform;
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
            if (other.TryGetComponent(out Player.Player player))
            {
                if (player.HealthComponent.IsDeath == false)
                {
                    _player = player;
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
            if (other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    _brick = brick;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Enemy) && tower.HealthComponent.IsDeath == false)
                {
                    if (tower.TowerLevel.IsMaxLevel == false)
                    {
                        _tower = tower.TowerLevel;
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
            if (other.TryGetComponent(out Player.Player player))
            {
                _player = null;
            }
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