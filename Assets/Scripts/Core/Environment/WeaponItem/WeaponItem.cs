using System;
using System.Collections.Generic;
using Components.Weapon;
using Core.Components._Spawners;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Environment.WeaponItem
{
    public class WeaponItem : ItemSpawn
    {
        [SerializeField] private List<WeaponData> _weapons;
        [SerializeField] private MeshFilter _meshFilter;
        private WeaponData _currentData;
        public WeaponData CurrentData => _currentData;
        private void Start()
        {
            _currentData = _weapons[Random.Range(0, _weapons.Count)];
            _meshFilter.mesh = _currentData.Mesh;
        }
    }
}