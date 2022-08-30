using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.OwnerRecruit;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using Core.Player;
using UnityEngine;

namespace Core.Characters.Player
{
    public class Player : Characters.Base.Character
    {
        [SerializeField] private PlayerMovement _movementController;
      
        [SerializeField] private DetachmentRecruit _detachment;
        [SerializeField] private DataProgressComponent _data;

        public PlayerMovement MovementController => _movementController;
        public DetachmentRecruit Detachment => _detachment;
        public DataProgressComponent DataProgress => _data;
    }
}