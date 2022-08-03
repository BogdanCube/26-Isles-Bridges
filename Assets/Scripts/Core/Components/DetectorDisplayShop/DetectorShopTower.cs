using Core.Components.DataTowers;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components.DetectorDisplayShop
{
    public class DetectorShopTower : MonoBehaviour
    {
        [SerializeField] private DataProgressComponent _data;
        [SerializeField] private Characters.Base.Character _character;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Tower tower) && other.TryGetComponent(out ShopTower shopTower))
            {
                if (tower.Owner == _character)
                {
                    var displayData = shopTower.DisplayProgress;
                    displayData.Load(_data.Сomponents,_data.Wallet);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Tower tower) && other.TryGetComponent(out ShopTower shopTower))
            {
                if (tower.Owner == _character)
                {
                    var displayData = tower.ShopTower.DisplayProgress;
                    displayData.Deload();
                }
            }
        }
    }
}