using Core.Components._ProgressComponents.Bag;
using Core.Components.Wallet;
using UnityEngine;

namespace Core.Character.Player
{
    public class Player : Characters.Base.Character
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Wallet _wallet;

        public Bag Bag => _bag;
        public Wallet Wallet => _wallet;
    }
}