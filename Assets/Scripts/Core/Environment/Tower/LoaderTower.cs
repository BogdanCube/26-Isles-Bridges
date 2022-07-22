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
        public void Load(int index)
        {
            Destroy(_currentTower.gameObject);
            var tower = _setting.Templates[index].Model;
            _currentTower = Instantiate(tower, transform);
            LoaderLevel.Instance.UpdateBake();
        }
    }
}