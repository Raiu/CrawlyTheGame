
namespace Con2D;
public class Player : Entity, IHealth, IDamageAble, ICombat
{
    private int _health;
    private int _attackDamage;
    private int _defence;

    public Player(int posX, int posY)
    {
        _posX = posX;
        _posY = posY;
        _oldPosX = posX;
        _oldPosY = posY;
        _isActive = true;
        _body = '@';

        _health = 100;
        _attackDamage = 10;
        _defence = 5;
    }

    public override void Move(int x, int y)
    {
        _oldPosX = _posX;
        _oldPosY = _posY;
        _posX = x;
        _posY = y;
    }

    public int HealthStatus() => _health;

    public void TakeDamage(int damage) => 
            _health -= (damage - _defence) > 0 ? damage - _defence : damage;

    public override void UpdateActive(bool status) => _isActive = status;

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
}
