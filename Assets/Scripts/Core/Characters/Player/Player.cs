using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.OwnerRecruit;
using Core.Components.Wallet;
using UnityEngine;

namespace Core.Characters.Player
{
    public class Player : Characters.Base.Character
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private DetachmentRecruit _detachment;

        public Bag Bag => _bag;
        public Wallet Wallet => _wallet;
        public DetachmentRecruit Detachment => _detachment;
    }
}