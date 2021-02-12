using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class AnimatedCharacterSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref AnimatedCharacterComponent character, ref PhysicsVelocity velocity, ref LocalToWorld tranform) =>
        {
            var animator = EntityManager.GetComponentObject<Animator>(character.AnimatorEntitiy);
            animator.SetFloat("speed", math.length(velocity.Linear));

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("forward", false);
            }
            else
            {
                animator.SetBool("forward", true);
            }
        });
    }
}