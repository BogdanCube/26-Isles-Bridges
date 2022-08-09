using Core.Components._Spawners;
using Core.Environment._ItemSpawn;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using Core.Environment.Tower;
using Core.Environment.Tower.NoBuilding;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class EnemyFinderOutside : DebugDetector
    {
        private Player.Player _player;
        private TowerLevel _tower;
        private Brick _brick;
        private RecruitItem _recruitItem;
        private ItemSpawn _item;
        private Island _island;
        private NoBuilding _noBuilding;
        public Player.Player Player => _player;
        public TowerLevel Tower => _tower;
        public Brick Brick => _brick;
        public ItemSpawn Item => _item;
        public Island Island => _island;
        public NoBuilding NoBuilding => _noBuilding;
        public bool IsTower => _tower && _tower.IsMaxLevel == false;
        public bool IsBrick => _brick && _brick.IsSet == false;
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                if (player.HealthComponent.IsDeath == false)
                {
                    _player = player;
                }
            }
            if (other.TryGetComponent(out TowerLevel tower) && other.TryGetComponent(out HealthTower _healthTower))
            {
                if (_healthTower.IsDeath == false && tower.IsMaxLevel == false)
                {
                    _tower = tower;
                }
            }
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                _noBuilding = noBuilding;
            }
            if (other.TryGetComponent(out Island island))
            {
                _island = island;
            }
            if (other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    _brick = brick;
                }
            }
            if (other.TryGetComponent(out ItemSpawn item))
            {
                _item = item;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _player = null;
            }
            /*if (other.TryGetComponent(out TowerLevel tower))
            {
                _tower = null;
            }*/
            if (other.TryGetComponent(out Island island))
            {
                _island = null;
            }
            if (other.TryGetComponent(out ItemSpawn blockItem))
            {
                _item = null;
            }
        }
    }
}