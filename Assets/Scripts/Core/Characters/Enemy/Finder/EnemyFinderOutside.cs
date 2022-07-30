using System;
using System.Collections.Generic;
using Core.Components._Spawners;
using Core.Environment._ItemSpawn;
using Core.Environment.Block;
using Core.Environment.Bridge;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using Core.Environment.Tower;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Characters.Enemy.Finder
{
    public class EnemyFinderOutside : FinderBase
    {
        [SerializeField] private Character.Player.Player _player;
        [SerializeField] private TowerLevel _tower;
        [SerializeField] private Brick _brick;
        [SerializeField] private RecruitItem _recruitItem;
        [SerializeField] private ItemSpawn _item;
        public Character.Player.Player Player => _player;
        public TowerLevel Tower => _tower;
        public Brick Brick => _brick;
        public ItemSpawn Item => _item;
        public bool IsTower => _tower && _tower.IsMaxLevel == false;
        public bool IsBrick => _brick && _brick.IsSet == false;
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player player))
            {
                if (player.HealthComponent.IsDeath == false)
                {
                    _player = player;
                }
            }
            if (other.TryGetComponent(out TowerLevel tower) && other.TryGetComponent(out HealthTower _healthTower))
            {
                if (_healthTower.IsDeath == false)
                {
                    _tower = tower;
                }
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
            if (other.TryGetComponent(out Base.Character character))
            {
                _player = null;
            }
            /*if (other.TryGetComponent(out TowerLevel tower))
            {
                _tower = null;
            }*/
            if (other.TryGetComponent(out ItemSpawn blockItem))
            {
                _item = null;
            }
        }
    }
}