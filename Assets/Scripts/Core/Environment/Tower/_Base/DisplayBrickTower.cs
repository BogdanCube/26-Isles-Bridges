using System.Collections;
using Core.Components._ProgressComponents.Bag;
using UnityEngine;

namespace Core.Environment.Tower._Base
{ 
    public class DisplayBrickTower : DisplayCountBrick
    {
        [SerializeField] private float _speed;
        protected override void SetBlock(Block.Block block)
        {
            base.SetBlock(block);
            /*block.MoveToTower(_bag.transform,_speed,() =>
            {
                RemoveBlock(block);
            });*/
        }
    }
}