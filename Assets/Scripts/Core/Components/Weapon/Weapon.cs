using System;
using Components.Weapon;
using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Components.Weapon
{
     public class Weapon : MonoBehaviour,IWeapon
     {
         [Expandable] [SerializeField] private WeaponData _currentData;
         [SerializeField] private HealthComponent _healthComponent;
         [SerializeField] private MeshFilter _meshFilter;
         private int _damage;
         private float _chanceVampirism;
         private float _chanceCritical;
         public Action OnTakeDamage;

         private void Start()
         {
             Load(_currentData);
         }

         public void Load(WeaponData weaponData)
         {
             _damage = weaponData.Damage;
             _chanceVampirism = weaponData.ChanceVampirism;
             _chanceCritical = weaponData.ChanceCritical;
             _meshFilter.mesh = weaponData.Mesh;
         }

         Action IWeapon.OnTakeDamage
         {
             get => OnTakeDamage;
             set => OnTakeDamage = value;
         }

         public void TakeDamage(Transform target, IHealthComponent health)
         {
             transform.DOLookAt(target.transform.position, 0.5f);
             health.Hit(_damage);     
             _healthComponent.Heal(CurrentHeal());
             OnTakeDamage?.Invoke();
         }
         private float CurrentDamage()
         {
             return _chanceCritical > Random.value ? _damage * 2 : _damage;
         }

         private int CurrentHeal()
         {
             return _chanceVampirism > Random.value ? _damage / 2 : 0;
         }

         
     }
}

