using Unity.Entities;

[GenerateAuthoringComponent]
public struct MagicFirePrefabsComponent : IComponentData
{
    public Entity Prefab;
    public float Speed;
}