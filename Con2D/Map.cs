using System.Diagnostics;

namespace Con2D;

public class Map
{
    public Tile[,] Tiles { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Map() : this(10, 10)
    {
    }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;

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
    
}
