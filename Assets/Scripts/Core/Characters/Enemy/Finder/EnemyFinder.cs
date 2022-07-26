using System;
using System.Collections.Generic;
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
        [SerializeField] private Components.Behavior.Character _character;
        [SerializeField] private Brick _brick;
        [SerializeField] private BlockItem _block;

        public Components.Behavior.Character Character => _character;
        public Brick Brick => _brick;
        public bool IsBlock => _block != null;
        public BlockItem Block => _block;


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Components.Behavior.Character character))
            {
                _character = character;
            }
            if (other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    _brick = brick;
                }
            }
            if (other.TryGetComponent(out BlockItem blockItem))
            {
                _block = blockItem;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Components.Behavior.Character character))
            {
                _character = null;
            }
            if (other.TryGetComponent(out BlockItem blockItem))
            {
                _block = null;
            }
        }
    }
}