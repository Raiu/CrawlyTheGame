using System.Diagnostics;

namespace Crawly;

public class Map
{
    public Tile[,] Tiles { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public int XStartIndex = 0;
    public int YStartIndex = 0;
    public int XEndIndex;
    public int YEndIndex;

    public Map() : this(10, 10)
    {
    }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;

        XEndIndex = Width - 1;
        YEndIndex = Height - 1;

        Tiles = new Tile[Width, Height];

        InitializeMap();

    }

    private void InitializeMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Tiles[x, y] = new Tile(TileType.Floor);
            }
        }
    }

    public bool IsAdjacentTiles(int firstX, int firstY, int secondX, int secondY)
    {
        return Math.Abs(firstX - secondX) <= 1 && Math.Abs(firstY - secondY) <= 1;
    }

    public bool IsAdjecent(int x1, int y1, int x2, int y2)
    {
        int dx = Math.Abs(x1 - x2);
        int dy = Math.Abs(y1 - y2);

        return dx <= 1 && dy <= 1 && dx + dy > 0;
    }

    public bool IsPositionWithinMap(int x, int y, int mapWidth, int mapHeight) =>
            x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;

    public Map CreateCopy()
    {
        var newMap = new Map(this.Width, this.Height)
        {
            XStartIndex = this.XStartIndex,
            YStartIndex = this.YStartIndex,
            XEndIndex = this.XEndIndex,
            YEndIndex = this.YEndIndex
        };

        for (int x = 0; x < this.Width; x++)
        {
            for (int y = 0; y < this.Height; y++)
            {
                newMap.Tiles[x, y] = this.Tiles[x, y].CreateCopy();
            }
        }

        return newMap;
    }
}
