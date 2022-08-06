using Core.Environment.WeaponItem;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components.DetectorItems
{
    public class DetectorWeaponItem : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon _weapon;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WeaponItem weaponItem))
            {
                weaponItem.MoveToCharacter(transform,() =>
                {
                    _weapon.Load(weaponItem.CurrentData);
                    NightPool.Despawn(weaponItem);
                });
               
            }
        }
    }
}