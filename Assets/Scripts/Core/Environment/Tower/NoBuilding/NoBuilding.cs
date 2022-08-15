using System;
using Core.Components.DataTowers;
using Core.Environment.Island;
using Rhodos.Toolkit.Extensions;
using UI.DisplayParameters;
using UnityEngine;

namespace Core.Environment.Tower.NoBuilding
{
    public class NoBuilding : MonoBehaviour
    {
        [SerializeField] private FreeIsland _freeIsland;
        [SerializeField] private DisplayDataTower _displayData;

        public FreeIsland FreeIsland => _freeIsland;
        public DisplayDataTower DisplayData => _displayData;

        public void Enable()
        {
            transform.Activate();
            _freeIsland.SetColor(Color.gray, 1);
        }
    }
}