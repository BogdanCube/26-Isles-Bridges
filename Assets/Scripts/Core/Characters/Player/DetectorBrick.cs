using System;
using Core.Environment.Bridge.Brick;
using Core.Player.Bag;
using UnityEngine;

namespace Core.Characters.Player
{
    public class DetectorBrick : MonoBehaviour
    {
        [SerializeField] private Bag _bag;

        private void OnTriggerEnter(Collider other)
        {
            if(_bag.HasCanSpend && other.TryGetComponent(out Brick brick))
            {
                if (brick.IsSet == false)
                {
                    _bag.Spend();
                    brick.SetBrick();
                }
            }
        }
    }
}