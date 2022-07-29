using System;
using System.Collections.Generic;
using Core.Components._Spawners;
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
    public class EnemyFinder : FinderBase
    {
        [SerializeField] private Character.Player.Player _player;
        [SerializeField] private TowerLevel _tower;
        [SerializeField] private Brick _brick;
        [SerializeField] private ItemSpawn _item;

        public Character.Player.Player Player => _player;
        public TowerLevel Tower => _tower;
        public Brick Brick => _brick;
        public ItemSpawn Item => _item;
        public bool IsTower => _tower && _tower.IsMaxLevel == false;
        public bool IsItem => _item && _item.enabled;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player character))
            {
                _player = character;
            }
            if (other.TryGetComponent(out TowerLevel tower))
            {
                _tower = tower;
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
            if (other.TryGetComponent(out TowerLevel tower))
            {
                _tower = null;
            }
            if (other.TryGetComponent(out ItemSpawn blockItem))
            {
                _item = null;
            }
        }
    }
}