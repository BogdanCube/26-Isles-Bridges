using Core.Environment.Block;
using UnityEngine;

namespace Core.Components.DetectorItems
{
    public class DetectorBlockItem : MonoBehaviour
    {
        [SerializeField] private _ProgressComponents.Bag.Bag _bag;
        private void OnTriggerEnter(Collider other)
        {
            if (_bag.HasCanAdd && other.TryGetComponent(out BlockItem block))
            {
                block.MoveToCharacter(transform,() =>
                {
                    _bag.Add();
                    block.SpendCount();
                });
            }
        }
    }
}