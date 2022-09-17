using System;
using System.Collections.Generic;
using Core.Components._Spawners;
using Core.Components.Weapon;
using NaughtyAttributes;
using Toolkit.Extensions;
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
            _currentData = _weapons.RandomItem();
            _meshFilter.mesh = _currentData.Mesh;
        }
    }
}