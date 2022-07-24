using System;
using Core.Components._ProgressComponents;

namespace Core.Environment.Tower
{
    public class LoaderTimeSpawn : ProgressComponent
    {
        public int TimeSpawn => _maxCount;
        private void Start()
        {
            Load();
        }
    }
}