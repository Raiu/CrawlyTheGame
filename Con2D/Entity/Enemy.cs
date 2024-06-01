
namespace Con2D;

public class Enemy : Entity, IHealth, IDamageAble, ICombat
{
    public Enemy(int posX, int posY)
    {
        _posX = posX;
        _posY = posY;
        _oldPosX = posX;
        _oldPosY = posY;
        _isActive = true;
        _body = '@';
    }

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

    public int HealthStatus()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
}