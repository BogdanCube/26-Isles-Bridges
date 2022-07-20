using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemy.Loot.Data
{
    [CreateAssetMenu(fileName = "New Loot Data", menuName = "ScriptableObjects/Loot/LootData", order = 1)]
    public class LootData: ScriptableObject
    {
        public List<LevelLootItems> Items;
    }
    [System.Serializable]
    public struct LevelLootItems
    {
        public List<DroppedItem> Items;
    }

    [System.Serializable]
    public struct DroppedItem
    {
        public ItemData Item;
        public int MaxAmount;

        public string GetInformation()
        {
            return  $"Предмет: {Item.Name}. " +
                    $" максимальное количество: {MaxAmount}";
        }
    }
}