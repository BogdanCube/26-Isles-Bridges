using Core.Characters.Recruit;
using Core.Environment._ItemSpawn;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components
{
    public class DetectorRecruit : MonoBehaviour
    {
        [SerializeField] private DetachmentRecruit _detachmentRecruit;
        private void OnTriggerEnter(Collider other)
        {
            if (_detachmentRecruit.HasCanAdd && other.TryGetComponent(out RecruitItem recruit))
            {
                _detachmentRecruit.Add(recruit.transform);
                NightPool.Despawn(recruit);
            }
        }
    }
}