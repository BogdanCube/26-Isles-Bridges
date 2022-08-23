using System;
using System.Collections;
using Base.Level;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower._Base;
using DG.Tweening;
using Managers.Level;
using NaughtyAttributes;
using NTC.Global.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Environment.Tower
{
    public class TowerLevel : MonoBehaviour
    {
        [SerializeField] private int _level;
        [SerializeField] private float _pumpingSpeed;
        [SerializeField] private Bag _shopBag;
        [SerializeField] private LoaderTower _loaderTower;
        [SerializeField] private ParticleSystem _particleLevel;
        private bool _isReserve = false;
        public Action<int,int,bool> OnUpdate;
        public Action<int,int,bool> OnHit;
        public Action OnMaxUpgrade;
        public bool IsMaxLevel => _level + 1 >= _loaderTower.MaxLevel;

        public void LoadTower()
        {
            UpdateDisplay();
            _loaderTower.Load(_level);
        }
        
        public IEnumerator ReplenishmentBrick(Bag bag,Action callback = null)
        {
            while (true)
            {
                if (bag.HasCanSpend())
                {
                    yield return new WaitForSeconds(_pumpingSpeed);
                    bag.Spend();
                    _shopBag.Add();
                    if (IsMaxLevel == false)
                    {
                        if (_shopBag.CurrentCount >= _loaderTower.PriceNextLevel(_level))
                        {
                            if (_isReserve)
                            {
                                _isReserve = false;
                                _shopBag.Reset();
                                _shopBag.Add();
                            }
                            else
                            {
                                LevelUp();
                            }
                        }
                    }
                    UpdateDisplay();
                }
                else
                {
                    callback?.Invoke();
                    break;
                }
            }
        }
        [Button]
        private void LevelUp()
        {
            if (IsMaxLevel == false)
            {
                _level++;
                _particleLevel.gameObject.SetActive(true);
                _loaderTower.Load(_level);
                transform.DOShakeScale(1f, Vector3.one/10f);
                if (IsMaxLevel)
                {
                    OnMaxUpgrade.Invoke();
                }
            }
            _shopBag.Reset();
            _shopBag.Add();
            UpdateDisplay();
        }
        private void LevelDown()
        {
            _level--;
            _loaderTower.Load(_level);
            _shopBag.Reset();
            _shopBag.Add(_loaderTower.PriceNextLevel(_level) - 1);
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            OnUpdate.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level),_isReserve);
        }
        public void Hit(int damage)
        {
            _shopBag.Spend(damage);
            OnHit.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level),_isReserve);
        }
        
        public void DestroyTower(Action callback)
        {
            if (_isReserve == false)
            {
                _isReserve = true;
                _shopBag.Reset();
                _shopBag.Add(_loaderTower.PriceNextLevel(_level) - 1);
                UpdateDisplay();
            }
            else
            {
                if (_level > 0)
                {
                    LevelDown();
                    //_isReserve = false;
                }
                else
                {
                    callback.Invoke();
                }
            }
        }
    }
}