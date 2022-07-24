using System;
using Core.Components.Wallet;
using UnityEngine;

namespace Core.Environment.Tower.ShopProgressItem
{
    public class DetectorWallet : MonoBehaviour
    {
        [SerializeField] private Canvas _panelBuy;
        [SerializeField] private TowerLevel _towerLevel;
        private Wallet _wallet;
        public Wallet Wallet => _wallet;
        private void Start()
        {
            _panelBuy.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Wallet wallet))
            {
                _wallet = wallet;
                _panelBuy.gameObject.SetActive(true);
                if (_towerLevel.IsMaxLevel == false)
                {
                    StartCoroutine(_towerLevel.ReplenishmentBlock(_wallet));
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Wallet wallet))
            {
                _wallet = null;
                _panelBuy.gameObject.SetActive(false);
                StopAllCoroutines();
            }
        }
    }
}