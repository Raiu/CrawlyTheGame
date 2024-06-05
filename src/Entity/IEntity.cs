namespace Crawly;

public interface IEntity
{
    Guid Id { get; }
    EntityType Type { get; }
    Coordinate Position { get; }
    bool IsActive { get; }

    void SetPosition();
}
