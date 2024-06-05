namespace Crawly;

public abstract class Entity
{
    protected EntityType _type;
    protected Coordinate _position;
    protected bool _isActive;
    protected char _body;

    public virtual EntityType Type
    {
        get => GetEntityType();
        set => SetEntityType(value);
    }
    protected virtual EntityType GetEntityType() => _type;
    protected virtual EntityType SetEntityType(EntityType value) => _type = value;

    public virtual Coordinate Position
    {
        get => GetPosition();
        set => SetPosition(value);
    }
    protected virtual Coordinate GetPosition() => _position;
    protected virtual Coordinate SetPosition(Coordinate value) => _position = value;

    public virtual bool IsActive
    {
        get => GetIsActive();
        set => SetIsActive(value);
    }
    protected virtual bool GetIsActive() => _isActive;
    protected virtual bool SetIsActive(bool value) => _isActive = value;   
}
