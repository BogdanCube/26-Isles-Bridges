using Managers.Level;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class LoaderTower : MonoBehaviour
    {
        [Expandable] [SerializeField] private SettingLevelTower _setting;
        [SerializeField] private Transform _currentTower;
        [SerializeField] private Transform _modelParent;
        public int MaxLevel => _setting.Templates.Count;
        public int PriceNextLevel(int level) => _setting.Templates[level].Price;

        public void Load(int index)
        {
            Destroy(_currentTower.gameObject);
            var tower = _setting.Templates[index].Model;
            _currentTower = Instantiate(tower, _modelParent);
            LoaderLevel.Instance.UpdateBake();
        }
    }
}