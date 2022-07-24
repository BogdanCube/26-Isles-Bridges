using Components.Health;
using Core.Components.Behavior;
using Core.Components.Health;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components.Weapon
{
     public class Weapon : MonoBehaviour
     {
         [SerializeField] private HealthComponent _healthComponent;
         [SerializeField] private MeshFilter _meshFilter;
         private int _damage;
         private float _chanceVampirism;
         private float _chanceCritical;
         
         public void TakeDamage(Character character)
         {
             character.HealthComponent.Hit(_damage);     
             _healthComponent.Heal(CurrentHeal());
         }
         private float CurrentDamage()
         {
             return _chanceCritical > Random.Range(0f, 1f) ? _damage * 2 : _damage;
         }

         private int CurrentHeal()
         {
             return _chanceVampirism > Random.Range(0f, 1f) ? _damage / 2 : 0;
         }

         public void Load(WeaponData weaponData)
         {
             _damage = weaponData.Damage;
             _chanceVampirism = weaponData.ChanceVampirism;
             _chanceCritical = weaponData.ChanceCritical;
             _meshFilter.mesh = weaponData.Mesh;
         }
     }
}

