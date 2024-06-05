namespace Crawly;

public struct Coordinate(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public static Coordinate operator + (Coordinate a, Coordinate b) =>
            new(a.X + b.X, a.Y + b.Y);

    public static bool operator == (Coordinate a, Coordinate b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator != (Coordinate a, Coordinate b)
    {
        return a.X != b.X || a.Y != b.Y;
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj is Coordinate other)
            return X == other.X && Y == other.Y;
        return false;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
        }
    }
}
