using System.Collections.Generic;
using System.Linq;
using Core.Components._ProgressComponents;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components.DataTowers
{
    public class DataProgressComponent : MonoBehaviour
    {
        [SerializeField] private List<ProgressComponent> _components;
        [SerializeField] private Wallet.Wallet _wallet;
        private ProgressComponent _lowPricedComponent;
        public List<ProgressComponent> Сomponents => _components;
        public Wallet.Wallet Wallet => _wallet;
        public ProgressComponent LowPricedComponent => _lowPricedComponent;

        [ShowNativeProperty] public bool CanBuySomething 
            => _components.Count > 0 &&
               _components.Count(component => component.Price <= _wallet.CurrentCount 
                                                         && component.IsMaxLevel == false) > 0;

        public void AddComponent(ProgressComponent progressComponent)
        {
            var list = new List<ProgressComponent> { progressComponent };
            _components.ForEach(component => list.Add(component));
            
            _components = list;
        }
    }
}