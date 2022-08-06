using Core.Environment.Tower.NoBuilding;
using UnityEngine;

namespace Core.Components.DetectorDisplayShop
{
    public class DetectorNoBuilding : MonoBehaviour
    {
        [SerializeField] private DataTowers.DataTowers _dataTowers;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                if (noBuilding.isLoaded == false)
                {
                    var displayData = noBuilding.DisplayData;
                    displayData.Load(_dataTowers.TowerData,_dataTowers.Bag);
                    noBuilding.isLoaded = true;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                if (noBuilding.isLoaded)
                {
                    var displayData = noBuilding.DisplayData;
                    displayData.Deload();
                    noBuilding.isLoaded = false;
                }
            }
        }
    }
}