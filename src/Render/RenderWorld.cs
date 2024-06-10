using System.Text;
using Spectre.Console;

namespace Crawly;

public class RenderWorld : IRender
{
    private Map _renderMap;
    
    private List<IEntity> _entities;
    
    private Player _player;

    private int _viewSize, _viewSizeHalf;
    
    private int _viewStartX, _viewEndX;
    
    private int _viewStartY, _viewEndY;

    ////////////////////////////////////////////////////////////////////////////////

    public RenderWorld(Map map, List<IEntity> entities, int viewSize)
    {
        _renderMap = map.CreateCopy();
        _entities = entities;

        if (_renderMap.Width < viewSize || _renderMap.Height < viewSize)
            throw new Exception("View size is too big");
        _viewSize = viewSize;
        _viewSizeHalf = (int)Math.Ceiling((double)(_viewSize - 1) / 2);

        var player = _entities.Find(x => x is Player) as Player ??
                throw new Exception("Player not found");
        _player = player;

        // var map = new JsonMapSerializer().Deserialize(_mapRenderBase);
        // map = UpdateTilesWithEntities(EntityManager.Drawables, map);
    }

    ////////////////////////////////////////////////////////////////////////////////

    private Map UpdateTilesWithEntities(List<IDrawable> entities, Map map)
    {
        entities.Where(e => e.IsVisible).ToList().ForEach(e =>
                map.Tiles[e.Position.X, e.Position.Y].UpdateEnity(e.Body));
        return map;
    }

    public void Draw()
    {
        SetWorldView();

        var renderedMap = RenderMapSpectre(_renderMap);

        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left").MinimumSize(100),
                new Layout("Right").MinimumSize(30));

        layout["Left"].Update(
            new Panel(
                Align.Center(
                    new Markup(renderedMap), VerticalAlignment.Middle))
                .Expand());
        layout["Right"].Update(new Panel(Align.Center(new Markup(GenPlayerString()))));

        AnsiConsole.Clear();

        AnsiConsole.Write(layout);
    }

    private void SetWorldView()
    {
        _viewStartX = _player.Position.X - _viewSizeHalf;
        _viewStartY = _player.Position.Y - _viewSizeHalf;
        _viewEndX = _viewStartX + _viewSize;
        _viewEndY = _viewStartY + _viewSize;

        if (_viewStartX < _renderMap.XStartIndex)
        {
            _viewStartX = _renderMap.XStartIndex;
            _viewEndX = _viewStartX + _viewSize;
        }
        if (_viewEndX > _renderMap.XEndIndex)
        {
            _viewEndX = _renderMap.XEndIndex;
            _viewStartX = _viewEndX - _viewSize;
        }
        if (_viewStartY < _renderMap.YStartIndex)
        {
            _viewStartY = _renderMap.YStartIndex;
            _viewEndY = _viewStartY + _viewSize;
        }
        if (_viewEndY > _renderMap.YEndIndex)
        {
            _viewEndY = _renderMap.YEndIndex;
            _viewStartY = _viewEndY - _viewSize;
        }
    }

    private int AdjustViewStart(int start, int min, int max, int size)
    {
        var half = (size - 1) / 2;
        if (start < min)
            start = min;
        if (start > max - half)
            start = max - half;
        return start;
    }

    private int AdjustStart(int start, int min, int max, int viewSize)
    {
        if (start < min)
        {
            return min;
        }
        if (start + viewSize > max)
        {
            return max - viewSize;
        }
        return start;
    }

    public void DrawWorld()
    {
        AnsiConsole.Clear();

        var renderedMap = RenderMapSpectre(_renderMap);

        var layout = new Layout("Root")
            .SplitColumns(
                new Layout("Left").MinimumSize(100),
                new Layout("Right").MinimumSize(30));

        layout["Left"].Update(
            new Panel(
                Align.Center(
                    new Markup(renderedMap), VerticalAlignment.Middle))
                .Expand());
        layout["Right"].Update(new Panel(Align.Center(new Markup(GenPlayerString()))));

        AnsiConsole.Write(layout);
    }

    private string GenPlayerString() => $"Player: X{_player.Position.X}, Y{_player.Position.Y}";

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
}
