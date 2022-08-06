using System;
using Core.Components.DataTowers;
using Core.Environment.Island;
using UI.DisplayParameters;
using UnityEngine;

namespace Core.Environment.Tower.NoBuilding
{
    public class NoBuilding : MonoBehaviour
    {
        [SerializeField] private FreeIsland _freeIsland;
        [SerializeField] private DisplayDataTower _displayData;
        [HideInInspector] public bool isLoaded;

        public FreeIsland FreeIsland => _freeIsland;
        public DisplayDataTower DisplayData => _displayData;

        private void OnEnable()
        {
            _freeIsland.SetColor(Color.gray, 1);
        }
    }
}