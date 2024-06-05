namespace Crawly;

public class Player : IEntity, IDrawable, IMoveable, IHealth, IDamageAble, ICombat
{
    private int _health;
    private int _attackDamage;
    private int _defence;

    public Guid Id { get; private set; }
    public EntityType Type { get; private set; }
    public bool IsActive { get; private set; }
    public Coordinate Position { get; set; }
    public Coordinate OldPosition { get; private set; }

    public char Body { get; private set; }
    public bool IsVisible { get; private set; }

    public Player (Coordinate position, char body = '@')
    {
        Type = EntityType.Player;
        IsActive = true;
        Position = position;
        OldPosition = Position;
        
        Body = '@';
        IsVisible = true;

        _health = 100;
        _attackDamage = 10;
        _defence = 5;
    }

    public void Move(Coordinate newPosition)
    {
        OldPosition = Position;
        Position = newPosition;
    }

    public int HealthStatus() => _health;

    public void TakeDamage(int damage) =>
            _health -= damage - _defence > 0 ? damage - _defence : damage;

    public void SetIsActive(bool status) => IsActive = status;

    public void Attack()
    {
        throw new NotImplementedException();
    }

    public void Defend()
    {
        throw new NotImplementedException();
    }

    public void Escape()
    {
        throw new NotImplementedException();
    }

    public List<CombatAction> GetActions()
    {
        throw new NotImplementedException();
    }

    public void SetPosition()
    {
        throw new NotImplementedException();
    }

    public void Draw()
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        throw new NotImplementedException();
    }
}
