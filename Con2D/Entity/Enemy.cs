namespace Con2D;

public class Enemy : Entity
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

    public override void Move(int x, int y)
    {
        _oldPosX = _posX;
        _oldPosY = _posY;
        _posX = x;
        _posY = y;
    }

    public override void UpdateActive(bool status) => _isActive = status;
}