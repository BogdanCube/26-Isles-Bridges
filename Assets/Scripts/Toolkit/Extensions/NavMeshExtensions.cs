using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Toolkit.Extensions
{
    public static class NavMeshExtensions
    {
        public static void SetRandomDestination(this NavMeshAgent navMeshAgent, float radius)
        {
            var randDirection = Random.insideUnitSphere * radius;
            randDirection += navMeshAgent.transform.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randDirection, out navHit, radius, -1);
            navMeshAgent.SetDestination(navHit.position);
        }
        
    }
}