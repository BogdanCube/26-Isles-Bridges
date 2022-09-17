using System;
using Core.Components.DataTowers;
using Core.Environment.Tower._Base;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Environment.Tower.ShopProgressItem
{
    public class DetectorDataProgress : MonoBehaviour
    {
        [SerializeField] private ShopTower _shopTower;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Characters.Player.Player player))
            {
                if (_shopTower.Owner == player && player.DataProgress)
                {
                    var displayData = _shopTower.DisplayProgress;
                    var data = player.DataProgress;
                    displayData.Load(data.Сomponents,data.Wallet);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Characters.Player.Player player))
            {
                if (_shopTower.Owner == player && player.DataProgress)
                {
                    var displayData = _shopTower.DisplayProgress;
                    if (!displayData) return;
                    displayData.Deload();
                }
            }
        }
    }
}