using Core.Characters.Recruit;
using UnityEngine;

namespace Core.Components
{
    public class DetectorRecruit : MonoBehaviour
    {
        [SerializeField] private DetachmentRecruit _detachmentRecruit;
        private void OnTriggerEnter(Collider other)
        {
            if (_detachmentRecruit.HasCanAdd && other.TryGetComponent(out FollowOwner recruit))
            {
                _detachmentRecruit.Add(recruit);
                recruit.SetTarget(transform);
            }
        }
    }
}