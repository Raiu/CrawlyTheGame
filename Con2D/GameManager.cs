using System.Text;
using Spectre.Console;

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
            //if (iter > 20) break;

            Map map = Serializer.Deserialize(_serializedMap);
            map.Tiles[0, 0].Type = TileType.Wall;
            map.Tiles[0, 1].Type = TileType.Wall;
            map.Tiles[0, 2].Type = TileType.Wall;

            map.Tiles[_player.PosX, _player.PosY].UpdateEnity(_player.Body);

            AnsiConsole.Clear();

            var renderedMap = RenderMapSpectre(map);

            var layout = new Layout("Root")
                .SplitColumns(
                    new Layout("Left"),
                    new Layout("Right"));

            layout["Left"].Update(
                new Panel(
                    Align.Center(
                        new Markup(renderedMap), VerticalAlignment.Middle))
                    .Expand());
            layout["Right"].Update( new Panel( Align.Center( new Markup($"Player: X{_player.PosX}, Y{_player.PosY}") ) ) );

            AnsiConsole.Write(layout);

            //Console.WriteLine($"Player: X{_player.PosX}, Y{_player.PosY}");

            MovePlayer(map);
            iter++;
        }
    }

    private void MovePlayer(Map map)
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
                    if (!ValidMove(_player.PosX, _player.PosY - 1, map, _entities))
                        break;
                    _player.Move(_player.PosX, _player.PosY - 1);
                    return;
                case InputKey.Down:
                    if (!ValidMove(_player.PosX, _player.PosY + 1, map, _entities))
                        break; 
                    _player.Move(_player.PosX, _player.PosY + 1);
                    return;
                case InputKey.Left:
                    if (!ValidMove(_player.PosX - 1, _player.PosY, map, _entities))
                        break;
                    _player.Move(_player.PosX - 1, _player.PosY);
                    return;
                case InputKey.Right:
                    if (!ValidMove(_player.PosX + 1, _player.PosY, map, _entities))
                        break;
                    _player.Move(_player.PosX + 1, _player.PosY);
                    return;
                case InputKey.Space:
                    Environment.Exit(1);
                    break;
                default:
                    break;

            }
        }
    }

    public bool ValidMove(int x, int y, Map map, List<Entity> entities)
    {
        if (!IsPositionWithinMap(x, y, map.Width, map.Height))
            return false;

        if (map.Tiles[x, y].Type == TileType.Wall)
            return false;

        if (entities.Any(e => e.PosX == x && e.PosY == y))
            return false;

        return true;                
    }

    public bool IsPositionWithinMap(int x, int y, int mapWidth, int mapHeight)
    {
        return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
    }

    private void UpdateEntities()
    {
        throw new NotImplementedException();
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

    public string RenderMapSpectre(Map map)
    { 
        var sb = new StringBuilder();
        
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                sb.Append(PrintTileSpectre(map.Tiles[x, y], x, y));
            }
            sb.Append("\n\n");
        }

        return sb.ToString();
    }


    private string PrintTileSpectre(Tile tile, int x, int y)
    {
        switch (tile.Type)
        {
            case TileType.Floor:
                return $" [default on lime]{x},{y}[/] ";
            case TileType.Wall:
                return $" [default on grey]{x},{y}[/] ";
            case TileType.EntityFloor:
                return $" [red on green]{x}*{y}[/] ";
            case TileType.EntityNone:
                return $" [red on green]{x}*{y}[/] ";
            default:
                return $" [default on lime]{x},{y}[/] ";
        }
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
