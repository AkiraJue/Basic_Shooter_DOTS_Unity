using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class AnimatedDeathCharacterSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref AnimatedCharacterComponent character, ref HealthComponent health) =>
        {
            var animator = EntityManager.GetComponentObject<Animator>(character.AnimatorEntitiy);
            if (health.Value <= 0)
            {
                animator.SetTrigger("die");
                EntityManager.RemoveComponent<HealthComponent>(entity);
            }
        });
    }
}