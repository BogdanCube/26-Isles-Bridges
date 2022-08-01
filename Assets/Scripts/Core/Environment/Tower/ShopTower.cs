using UI.DisplayParametrs;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class ShopTower : MonoBehaviour
    {
        [SerializeField] private DisplayProgressTower _display;
        public DisplayProgressTower DisplayProgress => _display;
    }
}