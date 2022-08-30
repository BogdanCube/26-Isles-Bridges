using UnityEngine;

namespace Toolkit.Extensions
{
    public static class TransformExtensions
    {
        
        public static float DistanceToTarget(this Transform transform, Transform target)
        {
            return Vector3.Distance(transform.position, target.position);
        }
        public static float DistanceToTarget(this Transform transform, Component target)
        {
            return Vector3.Distance(transform.position, target.transform.position);
        }
    }
}
