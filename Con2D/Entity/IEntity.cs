namespace Con2D;

public interface IEntity
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int OldPosX { get; set; }
    public int OldPosY { get; set; }
    public bool IsActive { get; set; }
    public char Body { get; set; }

    public void Move(int x, int y);

    public void UpdateActive(bool status);

}
