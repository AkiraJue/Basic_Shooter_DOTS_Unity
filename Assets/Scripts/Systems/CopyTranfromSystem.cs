using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class CopyTranfromSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref CopyTransfromComponent tag, ref LocalToWorld localToWorld) =>
        {
            var transform = EntityManager.GetComponentObject<Transform>(entity);
            transform.position = localToWorld.Position;
            transform.rotation = localToWorld.Rotation;
        });
    }
}