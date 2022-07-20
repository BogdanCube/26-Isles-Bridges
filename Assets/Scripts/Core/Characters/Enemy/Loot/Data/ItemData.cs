using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemy.Loot.Data
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "ScriptableObjects/Loot/ItemData", order = 1)]
    public class ItemData : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
        public List<Mesh> Meshs;
    }
}