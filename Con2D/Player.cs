namespace Con2D;

public class Player : Entity
{
    public Player(int posX, int posY)
    {
        PosX = posX;
        PosY = posY;
        OldPosX = posX;
        OldPosY = posY;
        IsActive = true;
        Body = '@';
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
