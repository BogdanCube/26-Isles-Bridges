using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class DetectorBag : MonoBehaviour
    {
        private Bag _currentBag;
        private void OnTriggerEnter(Collider other)
        {
            if ( other.TryGetComponent(out Bag bag))
            {
                if (bag.HasCanSpend)
                {
                    _currentBag = bag;
                }
            }        
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Bag bag))
            {
            }
        }
    }
}