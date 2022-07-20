using Core.Environment.Block;
using UnityEngine;

namespace Core.Player.Bag
{
    public class DetectorBlock : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Block block))
            {
               _bag.Add();
            }
        }
    }
}