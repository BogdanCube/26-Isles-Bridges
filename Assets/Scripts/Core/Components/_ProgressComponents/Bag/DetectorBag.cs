using System.Collections;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class DetectorBag : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerLevel towerLevel;
        private BagCharacter _bag;
        private IEnumerator _coroutine;
        private Characters.Base.Character _owner;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BagCharacter bag))
            {
                if (_tower.Owner == bag.Character && bag.HasCanSpend())
                {
                    _bag = bag;
                    if (towerLevel.IsMaxLevel == false)
                    {
                        _coroutine = towerLevel.ReplenishmentCoin(_bag);
                        StartCoroutine(_coroutine);
                    }
                }
            }        
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BagCharacter bag))
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
                _bag = null;
            }
        }
    }
}