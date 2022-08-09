using Core.Components._Spawners;
using NaughtyAttributes;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components.Wallet
{
    public class Coin : ItemSpawn
    {
        [MinMaxSlider(0, 50)] [SerializeField]
        private Vector2Int _count;
        
        public int RandomCount =>  _count.RandomRange();
        public override void SetSpawner(Spawner spawner, Vector3 position)
        {
            base.SetSpawner(spawner, position);
            transform.localScale = Vector3.one;
        }
    }
}