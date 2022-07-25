using System;
using Core.Components._ProgressComponents;
using Core.Components._Spawners;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class LoaderTimeSpawn : ProgressComponent
    {
        [SerializeField] private SpawnerBlockItem _spawnerBlockItem;
        [ShowNativeProperty] private int TimeSpawn => _maxCount;
        private void Awake()
        {
            Load();
            _spawnerBlockItem.StartSpawn(TimeSpawn);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            _spawnerBlockItem.StartSpawn(TimeSpawn);
        }
    }
}