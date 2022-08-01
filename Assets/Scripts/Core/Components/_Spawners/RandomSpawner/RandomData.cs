using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._Spawners.RandomSpawner
{
    [CreateAssetMenu(fileName = "New Random Data", menuName = "My Assets/Components/RandomData", order = 1)]
    public class RandomData : ScriptableObject
    {
        public List<TemplateSpawn> Templates;
    }
    [System.Serializable]
    public class TemplateSpawn
    {
        public ItemSpawn ItemSpawn;
        [MinMaxSlider(0, 25)] public Vector2Int CountSpawn;
    }
}