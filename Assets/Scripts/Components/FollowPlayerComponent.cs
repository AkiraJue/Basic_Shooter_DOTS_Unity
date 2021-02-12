using Unity.Entities;

[GenerateAuthoringComponent]
public struct FollowPlayerComponent : IComponentData
{
    public bool Disabled;
}