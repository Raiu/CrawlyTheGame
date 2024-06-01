namespace Con2D;

public enum EntityType
{
    Player,
    Enemy,
    Item,
    Door,
    Trap,
}

public abstract class Entity
{
    protected EntityType _type;
    protected int _posX, _posY;
    protected int _oldPosX, _oldPosY;
    protected bool _isActive;
    protected char _body;

    public virtual int PosX
    {
        get => GetPosX();
        set => SetPosX(value);
    }
    protected virtual int GetPosX() => _posX;
    protected virtual int SetPosX(int value) => _posX = value;

    public virtual int PosY
    {
        get => GetPosY();
        set => SetPosY(value);
    }
    protected virtual int GetPosY() => _posY;
    protected virtual int SetPosY(int value) => _posY = value;

    public virtual int OldPosX
    {
        get => GetOldPosX();
        set => SetOldPosX(value);
    }
    protected virtual int GetOldPosX() => _oldPosX;
    protected virtual int SetOldPosX(int value) => _oldPosX = value;

    public virtual int OldPosY
    {
        get => GetOldPosY();
        set => SetOldPosY(value);
    }
    protected virtual int GetOldPosY() => _oldPosY;
    protected virtual int SetOldPosY(int value) => _oldPosY = value;

    public virtual bool IsActive
    {
        get => GetIsActive();
        set => SetIsActive(value);
    }
    protected virtual bool GetIsActive() => _isActive;
    protected virtual bool SetIsActive(bool value) => _isActive = value;

    public virtual char Body
    {
        get => GetBody();
        set => SetBody(value);
    }
    protected virtual char GetBody() => _body;
    protected virtual char SetBody(char value) => _body = value;

    public virtual EntityType Type
    {
        get => GetEntityType();
        set => SetEntityType(value);
    }
    protected virtual EntityType GetEntityType() => _type;
    protected virtual EntityType SetEntityType(EntityType value) => _type = value;

    //

    public abstract void Move(int x, int y);

    public abstract void UpdateActive(bool status);
}
