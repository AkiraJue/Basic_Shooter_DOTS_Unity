using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;

public class FreezeVerticalRotationSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = Entities.ForEach((ref FreezeVerticalRotationComponent tag, ref PhysicsMass mass) =>
        {
            mass.InverseInertia.xz = new float2(0.0f);
        }).Schedule(inputDeps);

        return job;
    }
}