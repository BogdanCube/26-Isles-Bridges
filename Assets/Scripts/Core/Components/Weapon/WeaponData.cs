using JetBrains.Annotations;
using UnityEngine;

namespace Components.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "My Assets/Components/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        public int Damage;
        [Range(0,1)] public float ChanceVampirism;
        [Range(0,1)] public float ChanceCritical;


        [Header("Graphic Parameters")] [CanBeNull]
        public Mesh Mesh;
    }
    
}