using Core.Components.DataTowers;
using Core.Environment.Tower.NoBuilding;
using UnityEngine;

public class DetectorNoBuilding : MonoBehaviour
{
    [SerializeField] private DataTowers _dataTowers;
    private bool isLoaded;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out NoBuilding noBuilding))
        {
            if (isLoaded == false)
            {
                var displayData = noBuilding.DisplayData;
                displayData.Load(_dataTowers.TowerData, _dataTowers.Bag);
                isLoaded = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NoBuilding noBuilding))
        {
            var displayData = noBuilding.DisplayData;
            displayData.Deload();
            isLoaded = false;
        }
    }
}