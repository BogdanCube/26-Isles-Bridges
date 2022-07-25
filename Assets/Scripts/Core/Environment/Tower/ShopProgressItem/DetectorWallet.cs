using System;
using Core.Components.Wallet;
using UnityEngine;

namespace Core.Environment.Tower.ShopProgressItem
{
    public class DetectorWallet : MonoBehaviour
    {
        [SerializeField] private Canvas _panelBuy;
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
                
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Wallet wallet))
            {
                _panelBuy.gameObject.SetActive(false);
            }
        }
    }
}