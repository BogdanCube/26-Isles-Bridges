using Core.Characters._Base;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.OwnerRecruit;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using UnityEngine;

namespace Core.Characters.Player
{
    public class Player : Character
    {
        [SerializeField] private PlayerMovement _movementController;
      
        [SerializeField] private DataProgressComponent _data;

        public PlayerMovement MovementController => _movementController;
        public DataProgressComponent DataProgress => _data;
    }
}