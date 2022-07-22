using Core.Environment.Block;
using Core.Player.Bag;
using UnityEngine;

namespace Core.Components
{
    public class DetectorBlockItem : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        private void OnTriggerEnter(Collider other)
        {
            if (_bag.HasCanAdd && other.TryGetComponent(out BlockItem block))
            {
               _bag.Add();
               block.SpendCount();
            }
        }
    }
}