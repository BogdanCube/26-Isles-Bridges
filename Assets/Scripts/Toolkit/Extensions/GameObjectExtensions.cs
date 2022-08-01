using UnityEngine;

namespace Toolkit.Extensions
{
    public static class GameObjectExtensions
    {
        public static T Instantiate<T>(this T prefab) where T: Object
        {
            return Object.Instantiate<T>(prefab);
        }
        public static T Instantiate<T>(this T prefab, Transform transformInfo) where T : Object
        {
            return Object.Instantiate<T>(prefab, transformInfo.position, transformInfo.rotation);
        }
        
        public static T Instantiate<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Object
        {
            return Object.Instantiate<T>(prefab, position, rotation);
        }

        public static GameObject Instantiate(this GameObject prefab, Transform transformInfo, Transform parent)
        {
            return Object.Instantiate(prefab, transformInfo.position, transformInfo.rotation, parent);
        }
        public static GameObject Instantiate(this GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent, false);
        }
    }
}