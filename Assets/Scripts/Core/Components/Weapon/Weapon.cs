using System;
using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using NaughtyAttributes;
using Toolkit.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components.Weapon
{
     public class Weapon : MonoBehaviour,IWeapon
     {
         [Expandable] [SerializeField] private WeaponData _currentData;
         [SerializeField] private MeshFilter _meshFilter;
         private float _chanceVampirism;
         private float _chanceCritical;
         public event Action OnTakeDamage;
         private int Damage => _currentData.Damage;

         private void Start()
         {
             Load(_currentData);
         }

         public void Load(WeaponData weaponData)
         {
             _meshFilter.mesh = weaponData.Mesh;
         }
         
         public void TakeDamage(Transform target, IHealthComponent health)
         {
             transform.DOLookAt(target.transform.position, 0.5f);
             health.Hit(Damage);     
             OnTakeDamage?.Invoke();
         }
     }
}

