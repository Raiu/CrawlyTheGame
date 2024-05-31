namespace Con2D;

public class Render
{
    public void Draw()
    {
        Console.Clear();
    }

    public void Print(Map map)
    { 
        Console.BackgroundColor = ConsoleColor.White;
        
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                PrintTerrainCharacter(map.Tiles[x, y].Type);
            }
            Console.WriteLine();
        }

        Console.ResetColor();
    }

    private void PrintTerrainCharacter(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Floor:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("█");
                break;
            case TileType.Wall:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("█");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
                break;
        }
    }
}
