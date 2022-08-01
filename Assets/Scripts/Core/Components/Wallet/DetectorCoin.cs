using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components.Wallet
{
    public class DetectorCoin : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Coin coin))
            { 
                coin.MoveToCharacter(transform,() =>
                {
                    _wallet.Add(coin.RandomCount);
                    NightPool.Despawn(coin);
                });
            }
        }
    }
}