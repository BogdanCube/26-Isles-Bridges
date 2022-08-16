using Core.Environment._ItemSpawn.Block;
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
                _bag.Add();
                block.MoveToCharacter(transform);
            }
        }
    }
}