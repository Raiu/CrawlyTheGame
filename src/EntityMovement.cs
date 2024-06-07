namespace Crawly;

class EntityMovement
{
    public static Coordinate North => new Coordinate(x: 0, y: -1);
    public static Coordinate South => new Coordinate(x: 0, y: 1);
    public static Coordinate West => new Coordinate(x: -1, y: 0);
    public static Coordinate East => new Coordinate(x: 1, y: 0);
}