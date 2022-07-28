using Core.Components._Spawners;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components.Wallet
{
    public class Coin : ItemSpawn
    {
        [MinMaxSlider(0, 50)] [SerializeField]
        private Vector2Int _count;
        
        public int RandomCount =>  Random.Range(_count.x, _count.y);
 
    }
}