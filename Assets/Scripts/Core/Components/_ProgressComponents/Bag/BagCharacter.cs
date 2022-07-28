using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class BagCharacter : Bag
    {
        [SerializeField] private Characters.Base.Character _character;
        public Characters.Base.Character Character => _character;
    }
}