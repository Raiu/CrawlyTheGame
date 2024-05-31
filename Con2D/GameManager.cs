

namespace Con2D;

public class GameManager
{
    private JsonMapSerializer Serializer = new JsonMapSerializer();
    private Map _map;
    private string _serializedMap;

    private List<Entity> _entities;
    private Player _player;


    public GameManager()
    {
        _map = new Map(10, 10);
        _serializedMap = Serializer.Serialize(_map);
        
        _entities = new();
        _player = CreatePlayer(_map, _entities, '@');
        _entities.Add(_player);    
    }


    public void Run()
    {
        var iter = 0;
        while (true)
        {    
            if (iter > 20) break;

            Map map = Serializer.Deserialize(_serializedMap);
            map.Tiles[0, 0].Type = TileType.Wall;
            map.Tiles[0, 1].Type = TileType.Wall;
            map.Tiles[0, 2].Type = TileType.Wall;

            map.Tiles[_player.PosX, _player.PosY].UpdateEnity(_player.Body);

            Console.Clear();

            RenderDebug(map);

            Console.WriteLine($"{map.Width}x{map.Height}");
            Console.WriteLine($"Player: X{_player.PosX}, Y{_player.PosY}");

            MovePlayer();
            iter++;
        }
    }

    private void MovePlayer()
    {
        Input input = new Input();
        while (true)
        {
            var inputKey = input.ReadGameInput();
            if (inputKey == InputKey.None)
                continue;

            switch (inputKey)
            {
                case InputKey.Up:
                    _player.Move(_player.PosX, _player.PosY - 1);
                    return;
                case InputKey.Down:
                    _player.Move(_player.PosX, _player.PosY + 1);
                    return;
                case InputKey.Left:
                    _player.Move(_player.PosX - 1, _player.PosY);
                    return;
                case InputKey.Right:
                    _player.Move(_player.PosX + 1, _player.PosY);
                    return;
                default:
                    break;

            }
        }
    }

    private void UpdateEntities()
    {

    }


    private Player CreatePlayer(Map map, List<Entity> entities, char v)
    {
        var (posX, posY) = GenerateRandomPosition(map);
        var player = new Player(posX, posY);
        player.Body = v;
        return player;
    }


    private (int x, int y) GenerateRandomPosition(Map map)
    {
        if (ValidEntityPosition(map, 2, 2))
            return (1, 2);
        return (-1, -1);
    }


    private bool ValidEntityPosition(Map map, int x, int y)
    {
        return true;
    }


    public void Render(Map map)
    { 
        Console.BackgroundColor = ConsoleColor.White;
        
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                PrintTile(map.Tiles[x, y]);
            }
            Console.WriteLine();
        }

        Console.ResetColor();
    }


    private void PrintTile(Tile tile)
    {
        switch (tile.Type)
        {
            case TileType.Floor:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("▒");
                break;
            case TileType.Wall:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("█");
                break;
            case TileType.EntityFloor:
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(tile.Body);
                break;
            case TileType.EntityNone:
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ");
                break;
        }
        Console.BackgroundColor = ConsoleColor.White;
    }

    public void RenderDebug(Map map)
    { 
        Console.BackgroundColor = ConsoleColor.White;
        
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                PrintTileDebug(map.Tiles[x, y], x, y);
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
        }

        Console.ResetColor();
    }


    private void PrintTileDebug(Tile tile, int x, int y)
    {
        switch (tile.Type)
        {
            case TileType.Floor:
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {x},{y} ");
                Console.ResetColor();
                Console.Write("  ");
                break;
            case TileType.Wall:
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write($" {x},{y} ");
                Console.ResetColor();
                Console.Write("  ");
                break;
            case TileType.EntityFloor:
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" {x}*{y} ");
                Console.ResetColor();
                Console.Write("  ");
                break;
            case TileType.EntityNone:
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($" {x}*{y} ");
                Console.ResetColor();
                Console.Write("  ");
                break;
            default:
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write($" {x},{y} ");
                Console.ResetColor();
                Console.Write("  ");
                break;
        }
        Console.BackgroundColor = ConsoleColor.White;
    }
}
