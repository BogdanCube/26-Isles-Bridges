using System;
using System.Collections;
using System.Collections.Generic;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class DetectorBag : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerLevel towerLevel;
        private Bag _bag;
        private IEnumerator _coroutine;
        private Characters.Base.Character _owner;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bag bag))
            {
                if (CheckOwner(bag) && bag.HasCanSpend)
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

        private bool CheckOwner(Bag bag)
        {
            if (_owner == null)
            {
                _owner = bag.GetComponent<Characters.Base.Character>();
                return _tower.Owner == _owner;
            }
            else
            {
                return true;
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