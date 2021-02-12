using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;

public class MagicFireSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = Entities.ForEach((ref MagicFireComponent magicFire, ref PhysicsVelocity velocity) =>
        {
            velocity.Linear = magicFire.Speed;
        }).Schedule(inputDeps);

        return job;
    }
}