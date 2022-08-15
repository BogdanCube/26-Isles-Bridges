using System.Collections.Generic;
using Core.Environment.Block;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class DisplayCountBrick : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private protected Bag _bag;
        [SerializeField] private Block _prefab;
        private List<Block> _blocks = new List<Block>();
        [ShowNativeProperty] public int Count => _blocks.Count;
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
                int difference = count - _blocks.Count;
                for (int i = 0; i < difference; i++)
                {
                    AddBlock();
                }
            }
            else
            {
                int difference = _blocks.Count - count;
                for (int i = 0; i < difference; i++)
                {
                    RemoveBlock();
                }
            }
        }
    
        private void AddBlock()
        {
            var block =  NightPool.Spawn(_prefab, transform);
            SetBlock(block);
        }

        protected virtual void SetBlock(Block block)
        {
            block.SetPosition(GetNextHeight());
            _blocks.Add(block);
        }

        private void RemoveBlock()
        {
            if (_blocks.Count <= 0) return;
            var block = _blocks[^1];
            _blocks.Remove(block);
            NightPool.Despawn(block);

        }
        protected void RemoveBlock(Block block)
        {
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