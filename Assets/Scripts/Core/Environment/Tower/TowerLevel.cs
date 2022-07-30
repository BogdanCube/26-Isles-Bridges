using System;
using System.Collections;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
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
        
        public Action<int,int> OnUpdateDisplayed;
        public Action<int,int> OnHitDisplayed;
        public Action OnMaxUpgrade;
        public bool IsMaxLevel => _level + 1 >= _loaderTower.MaxLevel;

        private void Start()
        {
            UpdateDisplay();
            _loaderTower.Load(_level);
        }
        
        public IEnumerator ReplenishmentCoin(Bag bag)
        {
            while (IsMaxLevel == false)
            {
                yield return new WaitForSeconds(_pumpingSpeed);
                if (bag.HasCanSpend())
                {
                    bag.Spend();
                    _shopBag.Add();
                    if (_shopBag.CurrentCount >= _loaderTower.PriceNextLevel(_level))
                    {
                        LevelUp();
                    }
                    UpdateDisplay();
                }
            }
        }
        [Button]
        private void LevelUp()
        {
            if (IsMaxLevel == false)
            {
                _shopBag.Reset();
                _level++;
                //particle
                _loaderTower.Load(_level);
                UpdateDisplay();
                if (IsMaxLevel)
                {
                    _shopBag.Add();
                }
            }
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
            OnUpdateDisplayed.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level));
            if (IsMaxLevel)
            {
                OnMaxUpgrade.Invoke();
            }
        }
        public void Hit(int damage = 1)
        {
            if (_shopBag.HasCanSpend())
            { 
                _shopBag.Spend(damage);
                OnHitDisplayed.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level));
            }
        }

        public void DestroyTower()
        {
            if (_level > 0)
            {
                LevelDown();
            }
            else
            {
                transform.DOScale(0, 1f).OnComplete(() =>
                {
                    Destroy(gameObject);
                    LoaderLevel.Instance.UpdateBake();
                });
            }
        }
    }
}