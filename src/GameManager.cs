using System.Text;
using Spectre.Console;

namespace Crawly;

public enum GameCondition
{
    Running,
    Won,
    Lost,
}

public enum GameState
{
    None,
    MainMenu,
    InGameMenu,
    World,
    Combat
}

public class GameManager
{
    private JsonMapSerializer MapSerializer = new JsonMapSerializer();
    private Map _map;
    private string _mapRenderBase;

    private EntityManager EM;
    private Player Hero;

    private Player _combatPlayer;
    private Enemy _combatEnemy;

    private GameCondition _gameCondition;
    private GameState _gameState;

    public GameManager()
    {
        (EM, _map) = NewGame();

        _mapRenderBase = MapSerializer.Serialize(_map);

        Hero = EM.Hero;

        _gameState = GameState.World;
        _gameCondition = GameCondition.Running;

        Run();
    }

    public (EntityManager, Map) NewGame()
    {
        var map = new StaticMapGenerator().GenerateMap(30, 30);

        var em = new EntityManager(map);
        em.Add(EntityType.Enemy);

        return (em, map);
    }

    public void Run()
    {

        while (_gameCondition == GameCondition.Running)
        {
            switch (_gameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.InGameMenu:
                    break;
                case GameState.World:
                    RunWorld();
                    break;
                case GameState.Combat:
                    RunCombat();
                    break;
                default:
                    break;
            }
        }

        if (_gameCondition == GameCondition.Won)
            WinScreen();
    }

    private void RunMainMenu()
    {
        // Finish this last
        // New Game
        // Load Game
        // Map Editor
        // Quit

        throw new NotImplementedException();

        /*
        while (true)
        {
            var menu = new Dictionary<string, bool>
            {
                {"Play", false},
                {"Quit", false}
            };
        }
        */
    }

    private void RunInGameMenu()
    {
        // Inventory
        // Status
        throw new NotImplementedException();
    }

    private void RunWorld()
    {
        while (true)
        {
            if (HasWon())
            {
                _gameCondition = GameCondition.Won;
                break;
            }

            var IsCombat = CombatCheck(Hero, EM.Active);
            if (IsCombat) break;

            var map = MapSerializer.Deserialize(_mapRenderBase);
            map = UpdateTilesWithEntities(EM.Active, map);

            var render = new RenderWorld(map, EM.Active, 20);
            render.Draw();

            MovePlayer(map);
        }
    }

    private void RunCombat()
    {
        var CombatManager = new CombatManager(EM.Active, _combatPlayer, _combatEnemy);
        CombatManager.Run();

        _gameState = GameState.World;
    }

    private Dictionary<T, bool> CycleMenu<T>(Dictionary<T, bool> menu, int steps) where T : notnull
    {
        var index = menu.Values.ToList().IndexOf(true);

        if (index == -1)
            throw new InvalidOperationException("No item with value 'true' found in the menu.");

        var keys = menu.Keys.ToList();
        menu[keys[index]] = false;

        var newIndex = (index + steps) % keys.Count;
        newIndex = newIndex < 0 ? newIndex + keys.Count : newIndex;

        menu[keys[newIndex]] = true;

        return menu;
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
                    if (!ValidMove(Hero.PosX, Hero.PosY - 1, map, EM.Active))
                        break;
                    Hero.Move(Hero.PosX, Hero.PosY - 1);
                    return;
                case InputKey.Down:
                    if (!ValidMove(Hero.PosX, Hero.PosY + 1, map, EM.Active))
                        break;
                    Hero.Move(Hero.PosX, Hero.PosY + 1);
                    return;
                case InputKey.Left:
                    if (!ValidMove(Hero.PosX - 1, Hero.PosY, map, EM.Active))
                        break;
                    Hero.Move(Hero.PosX - 1, Hero.PosY);
                    return;
                case InputKey.Right:
                    if (!ValidMove(Hero.PosX + 1, Hero.PosY, map, EM.Active))
                        break;
                    Hero.Move(Hero.PosX + 1, Hero.PosY);
                    return;
                case InputKey.Esc:
                    Environment.Exit(1);
                    break;
                default:
                    break;

            }
        }
    }

    public bool CombatCheck(Player player, List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is Enemy)
            {
                if (!IsAdjacentTiles(player.PosX, player.PosY, entity.PosX, entity.PosY))
                    continue;

                _combatPlayer = player;
                _combatEnemy = (Enemy)entity;
                _gameState = GameState.Combat;
                return true;
            }

        }
        return false;
    }

    public bool HasWon() => !EM.Enemy.Any(e => e.IsActive);

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

    public bool ValidMove(int x, int y, Map map, List<Entity> entities)
    {
        if (!IsPositionWithinMap(x, y, map.Width, map.Height))
            return false;

        if (map.Tiles[x, y].Type == TileType.Wall)
            return false;

        if (entities.Any(e => e.PosX == x && e.PosY == y && e.IsActive))
            return false;

        return true;
    }

    private Map UpdateTilesWithEntities(List<Entity> entities, Map map)
    {
        entities.Where(e => e.IsActive).ToList().ForEach(e =>
                map.Tiles[e.PosX, e.PosY].UpdateEnity(e.Body));
        return map;
    }

    public bool IsPositionWithinMap(int x, int y, int mapWidth, int mapHeight) =>
            x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;


    private bool ValidEntityPosition(List<Entity> entities, int posX, int posY) =>
            !entities.Any(e => e.PosX == posX && e.PosY == posY);

    private void WinScreen()
    {
        Console.Clear();
        Console.WriteLine("YOU WON");

        Console.ReadKey(true);

        Environment.Exit(1);
    }
}
