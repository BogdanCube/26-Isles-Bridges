using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.Weapon
{
    public class LoaderWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Weapon _weapon;
        
        [Header("Graphic Parameters")] 
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private TrailRenderer _trailRenderer;

        private void Start()
        {
            LoadData();
        }

        [Button]
        private void LoadData()
        {
            _weapon.LoadParameters(_weaponData.Damage, _weaponData.ChanceVampirism, _weaponData.ChanceCritical);

            //meshFilter.transform.localPosition = weaponData.Offset;
            _meshFilter.mesh = _weaponData.Mesh;
            //_trailRenderer.colorGradient = weaponData.GradientTrail;
        }
    }
}