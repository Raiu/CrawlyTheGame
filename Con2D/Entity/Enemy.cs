namespace Con2D;

public class Enemy : Entity
{

    public Enemy(int posX, int posY)
    {
        PosX = posX;
        PosY = posY;
        OldPosX = posX;
        OldPosY = posY;
        IsActive = true;
        Body = 'E';
    }


    public override void Move(int x, int y)
    {
        OldPosX = PosX;
        OldPosY = PosY;
        PosX = x;
        PosY = y;
    }


    public override void UpdateActive(bool status) => IsActive = status;
}
