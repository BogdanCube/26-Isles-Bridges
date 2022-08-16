using System;
using Core.Characters.Enemy.Finder;
using Core.Components._ProgressComponents.Bag;
using Core.Environment.Bridge.Brick;
using UnityEngine;

namespace Core.Characters.Player
{
    public class DetectorBrick : MonoBehaviour
    {
        [SerializeField] private Bag _bag;

        private void OnTriggerStay(Collider other)
        {
            if(_bag.HasCanSpend() && other.TryGetComponent(out Brick brick))
            {
                _bag.Spend();
                brick.SetBrick();
            }
        }
    }
}