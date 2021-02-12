using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

public class CollisionEventSystem : JobComponentSystem
{
    private BuildPhysicsWorld _world;
    private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;
    private StepPhysicsWorld _step;

    private struct CollisionEventSystemJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<MagicFireComponent> MagicFireRef;
        public ComponentDataFromEntity<HealthComponent> HealthRef;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity hitEntity, magicFireEntity;
            if (MagicFireRef.HasComponent(triggerEvent.EntityA))
            {
                hitEntity = triggerEvent.EntityB;
                magicFireEntity = triggerEvent.EntityA;
            }
            else if (MagicFireRef.HasComponent(triggerEvent.EntityB))
            {
                hitEntity = triggerEvent.EntityA;
                magicFireEntity = triggerEvent.EntityB;
            }
            else
            {
                return;
            }

            var magicFire = MagicFireRef[magicFireEntity];
            magicFire.Destroyed = true;
            MagicFireRef[magicFireEntity] = magicFire;

            if (HealthRef.HasComponent(hitEntity))
            {
                var health = HealthRef[hitEntity];
                health.Value--;
                HealthRef[hitEntity] = health;
            }
        }
    }

    protected override void OnCreate()
    {
        _world = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        _step = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        // параллельная работа, которая проходит по всем триггерам;
        var job = new CollisionEventSystemJob();
        job.HealthRef = GetComponentDataFromEntity<HealthComponent>(isReadOnly: false);
        job.MagicFireRef = GetComponentDataFromEntity<MagicFireComponent>(isReadOnly: false);
        var jobResult = job.Schedule(_step.Simulation, ref _world.PhysicsWorld, inputDeps);

        var commandBuffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        // собираем команды на удаление Entity магических снарядов, которые были уничтожены;
        var result = Entities.ForEach((Entity entity, int entityInQueryIndex, ref MagicFireComponent magicFire) =>
        {
            if (magicFire.Destroyed)
            {
                commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }
        }).Schedule(jobResult);

        // настройка зависимостей;
        _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(result);
        return result;
    }
}