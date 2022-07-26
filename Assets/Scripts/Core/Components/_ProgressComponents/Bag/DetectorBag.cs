using System;
using System.Collections;
using System.Collections.Generic;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{

    public class DetectorBag : MonoBehaviour
    {
        [SerializeField] private TowerLevel _towerLevel;
        private Bag _bag;
        private IEnumerator _coroutine;
        
        private void OnTriggerEnter(Collider other)
        {
            if ( other.TryGetComponent(out Bag bag))
            {
                if (bag.HasCanSpend)
                {
                    _bag = bag;
                    if (_towerLevel.IsMaxLevel == false)
                    {
                        _coroutine = _towerLevel.ReplenishmentCoin(_bag);
                        StartCoroutine(_coroutine);
                    }
                }
            }        
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Bag bag))
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