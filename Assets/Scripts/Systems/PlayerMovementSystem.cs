using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        float2 input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Entities.ForEach((ref PlayerComponent player, ref LocalToWorld tranform, ref PhysicsVelocity velocity) =>
        {
            float3 direction = tranform.Forward * input.y * player.MovementSpeed * deltaTime;
            velocity.Linear += new float3(direction.x, 0.0f, direction.z);
            velocity.Angular = new float3(0.0f, input.x * player.RotationSpeed * deltaTime, 0.0f);
        });
    }
}