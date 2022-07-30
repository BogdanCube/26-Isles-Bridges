using Core.Environment.Tower.NoBuilding;
using UnityEngine;

namespace Core.Components
{
    public class DetectorNoBuilding : MonoBehaviour
    {
        [SerializeField] private DataTowers.DataTowers _dataTowers;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                var displayData = noBuilding.DisplayData;
                displayData.gameObject.SetActive(true);
                displayData.Load(_dataTowers.TowerData,_dataTowers.Bag);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out NoBuilding noBuilding))
            {
                var displayData = noBuilding.DisplayData;
                displayData.gameObject.SetActive(false);
                displayData.Deload();
            }
        }
    }
}