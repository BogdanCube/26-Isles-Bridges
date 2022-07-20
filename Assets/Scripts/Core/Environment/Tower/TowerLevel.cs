using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class TowerLevel : MonoBehaviour
    {
        [Range(0,5)] [SerializeField] private int _level;
        [SerializeField] private LoaderTower _loaderTower;
        
     
        [Button]
        private void LevelUp()
        {
            //_level++;
            //particle
            _loaderTower.Load(_level);
        }
    }
}