using Core.Enemy.Loot.Data;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Core.Enemy.Loot
{
    public class ItemTemplate : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        [Space] [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particleSystem;
        
        private ItemData _currentItem;
        public ItemData CurrentItem => _currentItem;
        public void Load(ItemData item)
        {
            _currentItem = item;
            _meshFilter.mesh = item.Meshs[Random.Range(0,item.Meshs.Count)];
            
            _rigidbody.AddForce(transform.up * 2);
            //_particleSystem.gameObject.SetActive(true);
        }

        public void DestroyItem()
        {
            Destroy(gameObject, Random.Range(1.5f,2f));
        }
    }
}