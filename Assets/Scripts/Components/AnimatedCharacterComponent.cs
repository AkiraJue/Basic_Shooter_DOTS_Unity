using Unity.Entities;

[GenerateAuthoringComponent]
public struct AnimatedCharacterComponent : IComponentData
{
    public Entity AnimatorEntitiy;
}