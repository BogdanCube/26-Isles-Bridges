using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class LoaderTower : MonoBehaviour
    {
        [Expandable] [SerializeField] private SettingLevelTower _setting;
        
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public void Load(int index)
        {
            var currentTower = _setting.Templates[index];
            _meshFilter.mesh = currentTower.Mesh;
            _meshRenderer.materials = currentTower.Materials;
        }
    }
}