using Core.Environment._ItemSpawn;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components._ProgressComponents.OwnerRecruit
{
    public class DetectorRecruit : MonoBehaviour
    {
        [SerializeField] private DetachmentRecruit _detachmentRecruit;
        [SerializeField] private Wallet.Wallet _wallet;
        [MinMaxSlider(0f, 25f)] [SerializeField] private Vector2Int _additionalCoin;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out RecruitItem recruit))
            {
                if (_detachmentRecruit.HasCanAdd)
                {
                    _detachmentRecruit.Add();
                }
                else
                {
                    var count = Random.Range(_additionalCoin.x, _additionalCoin.y);
                    _wallet.Add(count);
                }
                NightPool.Despawn(recruit);

            }
            
        }
    }
}