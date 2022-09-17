using Core.Characters._Base;
using Core.Components._ProgressComponents.Health;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components._ProgressComponents.Bag
{
    public class BagCharacter : Bag
    {
        [SerializeField] private Character _character;
        public Character Character => _character;
    }
}