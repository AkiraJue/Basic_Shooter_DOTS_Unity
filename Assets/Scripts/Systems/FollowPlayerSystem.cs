using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.AI;

public class FollowPlayerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float3 targetPosition = float3.zero;

        Entities.ForEach((Entity entity, ref FollowTargetComponent tag, ref LocalToWorld tranform) =>
        {
            targetPosition = tranform.Position;
        });

        Entities.ForEach((Entity entity, ref NavMashAgentComponent agent) =>
        {
            var navMashAgent = EntityManager.GetComponentObject<NavMeshAgent>(entity);
            if (navMashAgent != null)
            {
                navMashAgent.SetDestination(targetPosition);

                EntityManager.SetComponentData(agent.Entity, new Translation { Value = navMashAgent.transform.position });
            }
        });
    }
}