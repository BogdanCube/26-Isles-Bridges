using System.Collections.Generic;
using Core.Components._ProgressComponents.Health;
using Core.Components._Spawners;
using Core.Environment._ItemSpawn;
using Core.Environment.Block;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using Core.Environment.Tower;
using Core.Environment.Tower.NoBuilding;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class EnemyFinderOutside : DebugDetector
    {
        public Transform Target { get; private set; }
        [ShowNativeProperty] public Transform Tower { get; private set; }
        public Transform Brick { get; private set; }
        public Transform Item { get; private set; }
        public Transform BlockItem { get; private set; }
        public Transform NoBuilding { get; private set; }
        private List<Tower> _ignoreTowers = new List<Tower>();
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                if (player.HealthComponent.IsDeath == false)
                {
                    Target = player.transform;
                }
            }
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner.GetType() == typeof(Player.Player))
                {
                    if (tower.HealthComponent.IsDeath == false)
                    {
                        Target = tower.transform;
                    }
                }
                else if (tower.Owner.GetType() == typeof(Enemy) && tower.HealthComponent.IsDeath == false)
                {
                    if (_ignoreTowers.Contains(tower) == false)
                    {
                        if (tower.TowerLevel.IsMaxLevel)
                        {
                            _ignoreTowers.Add(tower);
                            Tower = null;
                        }
                        else
                        {
                            Tower = tower.transform;
                        }
                    }
                }
                
            }
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                NoBuilding = noBuilding.transform;
            }
            if (other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    Brick = brick.transform;
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

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                Target = null;
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