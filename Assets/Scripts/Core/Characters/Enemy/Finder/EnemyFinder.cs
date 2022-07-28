using System;
using System.Collections.Generic;
using Core.Components._Spawners;
using Core.Environment.Block;
using Core.Environment.Bridge;
using Core.Environment.Bridge.Brick;
using Core.Environment.Island;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Characters.Enemy.Finder
{
    public class EnemyFinder : FinderBase
    {
        [SerializeField] private Character.Player.Player _player;
        [SerializeField] private Brick _brick;
        [SerializeField] private ItemSpawn _item;

        public Character.Player.Player Player => _player;
        public Brick Brick => _brick;
        public bool IsItem => _item.enabled;
        public ItemSpawn Item => _item;


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Character.Player.Player character))
            {
                _player = character;
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
            if (other.TryGetComponent(out ItemSpawn blockItem))
            {
                _item = null;
            }
        }
    }
}