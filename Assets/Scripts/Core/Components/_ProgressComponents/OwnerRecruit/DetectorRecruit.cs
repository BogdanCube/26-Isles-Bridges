using Core.Environment._ItemSpawn;
using NaughtyAttributes;
using NTC.Global.Pool;
using Toolkit.Extensions;
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
                    recruit.MoveToCharacter(transform, _detachmentRecruit.Add);
                }
                else
                {
                    recruit.PickUp(() =>
                    {
                        _detachmentRecruit.OnMax.Invoke();
                        var count = _additionalCoin.RandomRange();
                        _wallet.Add(count);
                    });
                }
            }
        }
    }
}