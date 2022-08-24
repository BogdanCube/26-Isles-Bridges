using System.Collections.Generic;
using Core.Components._Spawners;
using UnityEngine;

namespace Toolkit.Extensions
{
    public static class RandomExtensions
    {
        public static T RandomItem<T>(this IList<T> list)
        {
            return list.Count switch
            {
                0 => throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list"),
                1 => list[0],
                _ => list[UnityEngine.Random.Range(0, list.Count)]
            };
        }

        public static float RandomRange(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);;
        }
        public static int RandomRange(this Vector2Int vector)
        {
            return Random.Range(vector.x, vector.y);;

        }
    }
}