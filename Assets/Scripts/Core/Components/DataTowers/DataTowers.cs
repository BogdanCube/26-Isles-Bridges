using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components.DataTowers
{
    public class DataTowers : MonoBehaviour
    {
        [Expandable] [SerializeField] private TowerData _towerData;
        [SerializeField] private _ProgressComponents.Bag.BagCharacter _bag;
        public TowerData TowerData => _towerData;
        public _ProgressComponents.Bag.BagCharacter Bag => _bag;
        public bool CanBuySomething => _towerData.Templates.Count(template => _bag.CurrentCount >= template.Price) > 0;

    }
}