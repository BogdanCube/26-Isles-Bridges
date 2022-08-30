using System;
using System.Collections;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower._Base
{
    public class TowerLevel : MonoBehaviour
    {
        [SerializeField] private int _level;
        [SerializeField] private float _pumpingSpeed;
        [SerializeField] private Bag _shopBag;
        [SerializeField] private LoaderTower _loaderTower;
        [SerializeField] private ParticleSystem _particleLevel;
        public Action<int,int> OnUpdate;
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
                            LevelUp();
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
            OnUpdate.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level));
        }
        
        public void DestroyTower(Action callback)
        {
            if (_level > 0)
            {
                LevelDown();
            }
            else
            {
                callback.Invoke();
            }
        }
    }
}