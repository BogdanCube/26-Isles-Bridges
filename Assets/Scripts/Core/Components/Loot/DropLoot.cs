using System;
using Core.Components.Health;
using Core.Enemy.Loot.Data;
using Random = UnityEngine.Random;
using UnityEngine;

namespace Core.Enemy.Loot
{
    public class DropLoot : MonoBehaviour
    {
        [SerializeField] private LootData _lootData;
        [SerializeField] private ItemTemplate _itemTemplate;
        
        [Space] [SerializeField] private HealthComponent _healthComponent;

        /*private void OnEnable()
        {
            _healthComponent.OnDeathEvent += Drop;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeathEvent -= Drop;
        }

        private void Drop()
        {
            var currentLootItems = _lootData.Items[_levelEquipment.Level];
            foreach (var droppedItem in currentLootItems.Items)
            {
                Spawn(droppedItem);
            }
            
        }*/
        private void Spawn(DroppedItem droppedItem)
        {
            int amount = Random.Range(0, droppedItem.MaxAmount);

            for (int i = 0; i < amount; i++)
            {
                var itemTemplate = Instantiate(_itemTemplate, transform.position, Quaternion.identity);     
                itemTemplate.Load(droppedItem.Item);
            }

        }
    }
}