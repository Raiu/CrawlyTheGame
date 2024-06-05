namespace Crawly;

public interface IMoveable
{
    public Coordinate OldPosition { get; }

    public void Move();
}
