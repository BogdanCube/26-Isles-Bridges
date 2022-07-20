using System;
using System.Collections.Generic;
using Core.Enemy.Loot.Data;
using Core.Environment.Block;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Core.Player.Bag
{
    public class DisplayBag : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private Bag _bag;
        [SerializeField] private Block _prefab;
        private List<Block> _blocks = new List<Block>();
        
        #region Enable/Disable
        private void OnEnable()
        {
            _bag.OnUpdateBag += UpdateBag;
        }

        private void OnDisable()
        {
            _bag.OnUpdateBag -= UpdateBag;
        }
        #endregion


        private void UpdateBag(int count)
        {
            if (count > _blocks.Count)
            {
                for (int i = 0; i < count - _blocks.Count; i++)
                {
                    AddBlock();
                }
            }
            else
            {
                for (int i = 0; i < _blocks.Count - count; i++)
                {
                    RemoveBlock();
                }
            }
        }
    
        private void AddBlock()
        {
            var block =  NightPool.Spawn(_prefab, transform);
            block.SetPosition(GetNextHeight());
            _blocks.Add(block);
        }

        private void RemoveBlock()
        {
            var block = _blocks[^1];
            
            _blocks.Remove(block);
            NightPool.Despawn(block);
        }

        private Vector3 GetNextHeight()
        {
            var height = _blocks.Count * _offset + _offset;
            return new Vector3(0, height, 0);
        }
    }
}