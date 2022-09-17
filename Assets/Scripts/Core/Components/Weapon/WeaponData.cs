using JetBrains.Annotations;
using NaughtyAttributes;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Components.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "My Assets/Components/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [MinMaxSlider(0,10)] [SerializeField] private Vector2Int _damage;
        
        [CanBeNull] [ShowAssetPreview] public Mesh Mesh;
        public int Damage => _damage.RandomRange();
    }
    
}