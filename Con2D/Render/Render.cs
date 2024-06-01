using System.Reflection.Metadata.Ecma335;
using System.Text;
using Spectre.Console;

namespace Con2D;

public class Render : IRender
{
    private Map _map;
    private List<Entity> _entities;
    private Player _player;

    private int _viewSize, _viewSizeHalf;
    private int _viewStartX, _viewEndX;
    private int _viewStartY, _viewEndY;

    public Render(Map map, List<Entity> entities, int viewSize)
    {
        _map = map;
        _entities = entities;

        if ( _map.Width < viewSize || _map.Height < viewSize) 
                throw new Exception("View size is too big");
        _viewSize = viewSize;
        _viewSizeHalf = (_viewSize - 1) / 2;

        var player = _entities.Find(x => x is Player) as Player ?? 
                throw new Exception("Player not found");
        _player = player;

        SetWorldView();
    }

    private void SetWorldView()
    {
        _viewStartX = _player.PosX - _viewSizeHalf;
        _viewStartY = _player.PosY - _viewSizeHalf;
        _viewEndX = _viewStartX + _viewSize;
        _viewEndY = _viewStartY + _viewSize;

        if (_viewStartX < _map.StartX) {
            _viewStartX = _map.StartX;
            _viewEndX = _viewStartX + _viewSize;
        }
        if (_viewEndX > _map.EndX) {
            _viewEndX = _map.EndX;
            _viewStartX = _viewEndX - _viewSize;
        }
        if (_viewStartY < _map.StartY) {
            _viewStartY = _map.StartY;
            _viewEndY = _viewStartY + _viewSize;
        }
        if (_viewEndY > _map.EndY) {
            _viewEndY = _map.EndY;
            _viewStartY = _viewEndY - _viewSize;
        }
    }

    public void DrawWorld()
    {
        AnsiConsole.Clear();

        var renderedMap = RenderMapSpectre(_map);

        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left").MinimumSize(100),
                new Layout("Right").MinimumSize(30));

        layout["Left"].Update(
            new Panel(
                Align.Center(
                    new Markup(renderedMap), VerticalAlignment.Middle))
                .Expand());
        layout["Right"].Update( new Panel( Align.Center( new Markup(GenPlayerString()) ) ) );

        AnsiConsole.Write(layout);
    }

    private string GenPlayerString() => $"Player: X{_player.PosX}, Y{_player.PosY}";

    public string RenderMapSpectre(Map map)
    { 
        var sb = new StringBuilder();

        
        for (var y = _viewStartY; y <= _viewEndY; y++)
        {
            for (var x = _viewStartX; x <= _viewEndX; x++)
            {
                sb.Append(PrintTileSpectre(map.Tiles[x, y], x, y));
            }
            sb.Append("\n");
        }

        return sb.ToString();
    }

    private string PrintTileSpectre(Tile tile, int x, int y)
    {
        switch (tile.Type)
        {
            case TileType.Floor:
                return $"[lime on lime]▒▒[/]";
            case TileType.Wall:
                return $"[grey on grey]██[/]";
            case TileType.EntityFloor:
                return $"[red on green]<{tile.Body}[/]";
            case TileType.EntityNone:
                return $"[red on green]<{tile.Body}[/]";
            default:
                return $"[lime on lime]▒[/]";
        }
    }


    private string PrintTileSpectreDebug(Tile tile, int x, int y)
    {
        var xString = x.ToString("00");
        var yString = y.ToString("00");

        switch (tile.Type)
        {
            case TileType.Floor:
                return $" [default on lime]{xString},{yString}[/]";
            case TileType.Wall:
                return $" [default on grey]{xString},{yString}[/]";
            case TileType.EntityFloor:
                return $" [red on green]{xString}*{yString}[/]";
            case TileType.EntityNone:
                return $" [red on green]{xString}*{yString}[/]";
            default:
                return $" [default on lime]{xString},{yString}[/]";
        }
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

    public void Draw()
    {
        throw new NotImplementedException();
    }
}
