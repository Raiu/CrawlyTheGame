namespace Crawly;

public interface IDrawable
{
    char Body { get; }
    bool IsVisible { get; }
    Coordinate Position { get; }

    void Draw();
}
