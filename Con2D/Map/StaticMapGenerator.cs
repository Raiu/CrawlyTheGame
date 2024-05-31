namespace Con2D;

public class StaticMapGenerator : IMapGenerator
{
    public Map GenerateMap(int width, int height)
    {
        var map = new Map(width, height);

        for (int i = 0; i < map.Width; i++)
        {
            map.Tiles[i, 0].Type = TileType.Wall;
            map.Tiles[i, map.Height - 1].Type = TileType.Wall;
            map.Tiles[0, i].Type = TileType.Wall;
            map.Tiles[map.Width - 1, i].Type = TileType.Wall;
        }

        for (int i = 1; i < 6; i++)
        {
            map.Tiles[5, i].Type = TileType.Wall;   
        }

        map.Tiles[1, 5].Type = TileType.Wall;
        map.Tiles[2, 5].Type = TileType.Wall;
        map.Tiles[3, 5].Type = TileType.Wall;



        return map;
    }
}
