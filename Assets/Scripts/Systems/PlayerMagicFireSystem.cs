using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class PlayerMagicFireSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Entities.ForEach((Entity entity, ref MagicFirePrefabsComponent magicFirePrefab) =>
            {
                var shooter = EntityManager.GetComponentObject<Shooter>(entity);
                if (shooter == null)
                {
                    Debug.LogError("MagicFirePrefabComponent is missing Shooter component");
                }
                else
                {
                    Entity magicFire = EntityManager.Instantiate(magicFirePrefab.Prefab);
                    EntityManager.SetComponentData(magicFire, new Translation { Value = shooter.MagicHole.position });
                    EntityManager.SetComponentData(magicFire, new Rotation { Value = shooter.MagicHole.rotation });
                    EntityManager.SetComponentData(magicFire, new MagicFireComponent { Speed = shooter.MagicHole.forward * magicFirePrefab.Speed });
                    ;
                }
            });
        }
    }
}