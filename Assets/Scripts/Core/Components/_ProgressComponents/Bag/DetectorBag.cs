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

        #region Enable / Disable
        private void OnEnable()
        {
            _towerLevel.OnMaxUpgrade += StopMoved;
        }

        private void OnDisable()
        {
            _towerLevel.OnMaxUpgrade -= StopMoved;
        }
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BagCharacter bag))
            {
                if (_tower.Owner == bag.Character && bag.HasCanSpend())
                {
                    if (_towerLevel.IsMaxLevel == false)
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
        private void StopMoved()
        {
            _tempBag.Reset();
            StopCoroutine(_coroutineSpend);
            StopCoroutine(_coroutineAdd);
        }
    }
}