using Components.Weapon;
using Core.Environment.Block;
using Core.Environment.WeaponItem;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components
{
    public class DetectorWeaponItem : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WeaponItem weaponItem))
            {
                _weapon.Load(weaponItem.CurrentData);
                NightPool.Despawn(weaponItem);
            }
        }
    }
}