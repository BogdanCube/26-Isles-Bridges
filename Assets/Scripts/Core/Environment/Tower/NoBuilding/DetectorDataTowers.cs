using System;
using Core.Components.DataTowers;
using UI.DisplayParametrs;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class DetectorDataTowers : MonoBehaviour
    {
        [SerializeField] private NoBuilding.NoBuilding _noBuilding;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DataTowers dataTowers))
            {
                var displayData = _noBuilding.DisplayData;
                displayData.gameObject.SetActive(true);
                displayData.Load(dataTowers.TowerData,dataTowers.Bag);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out DataTowers dataTowers))
            {
                var displayData = _noBuilding.DisplayData;
                displayData.gameObject.SetActive(false);
                displayData.Deload();
            }
        }
    }
}