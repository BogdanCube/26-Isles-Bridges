using System;
using System.Collections;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components._ProgressComponents.Bag
{
    public class DetectorBag : MonoBehaviour
    {
        [SerializeField] private float _pumpingSpeed = 0.1f;
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private Bag _tempBag;
        private IEnumerator _coroutineAdd;
        private IEnumerator _coroutineSpend;
        private Characters.Base.Character _owner;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BagCharacter bag))
            {
                if (_tower.Owner == bag.Character && bag.HasCanSpend())
                {
                    if (_coroutineSpend != null)
                    {
                        StopCoroutine(_coroutineSpend);
                    }
                    _coroutineAdd = _tempBag.MovedCount(bag,_pumpingSpeed);
                    StartCoroutine(_coroutineAdd);
                }
            }        
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BagCharacter bag))
            {
                if (_coroutineAdd != null)
                {
                    StopCoroutine(_coroutineAdd);
                }
                _coroutineSpend = _towerLevel.ReplenishmentBrick(_tempBag);
                StartCoroutine(_coroutineSpend);
            }
        }
    }
}