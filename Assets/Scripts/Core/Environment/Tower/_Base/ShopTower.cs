using Core.Characters._Base;
using UI.DisplayParameters;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Environment.Tower._Base
{
    public class ShopTower : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        [SerializeField] private DisplayProgressTower _display;
        public DisplayProgressTower DisplayProgress => _display;
        public Character Owner => _tower.Owner;
    }
}