using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MagicFireComponent : IComponentData
{
    public float3 Speed;
    public bool Destroyed;
}