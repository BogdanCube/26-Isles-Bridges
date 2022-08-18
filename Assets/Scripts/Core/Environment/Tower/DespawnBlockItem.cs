using System;
using Core.Environment._ItemSpawn.Block;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class DespawnBlockItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BlockItem blockItem))
            {
                blockItem.SpendCount();
            }
        }
    }
}