using System;
using System.Collections;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

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
                if (bag.HasCanSpend)
                {
                    bag.Spend();
                    _shopBag.Add();
                    if (_shopBag.CurrentCount >= _loaderTower.PriceNextLevel(_level))
                    {
                        LevelUp();
                        _shopBag.Reset();
                    }
                    UpdateDisplay();
                }
            }
        }
        [Button]
        private void LevelUp()
        {
            _level++;
            //particle
            _loaderTower.Load(_level);
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
            OnUpdateDisplayed.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level));
            if (IsMaxLevel)
            {
                OnMaxUpgrade.Invoke();
            }
        }
        public void Hit(int damage = 1)
        {
            if (_shopBag.HasCanSpend)
            { 
                _shopBag.Spend(damage);
                OnHitDisplayed.Invoke(_shopBag.CurrentCount, _loaderTower.PriceNextLevel(_level));
            }
            else
            {
                if (_level > 0)
                {
                    LevelDown();
                }
                else
                {
                    print("Death Tower");
                }
            }
        }
    }
}