using System.Text;
using Spectre.Console;

namespace Con2D;

public enum GameState
{ 
    Running,
    Won,
    Lost,
}

public enum ViewState
{
    MainMenu,
    World,
    Combat
}

public class GameManager
{
    private JsonMapSerializer MapSerializer = new JsonMapSerializer();
    private Map _map;
    private string _serializedMap; 

    private EntityManager EM;

    private List<Entity> _entities;
    private Player _player;


    public GameManager()
    {
        EM = new EntityManager();
        var enemy = (Enemy)EM.Add(EntityType.Enemy);
        var hero = EM.Hero;


        RunCombat(hero, enemy);

        /*
        EM = new EntityManager();
        _map = new StaticMapGenerator().GenerateMap(30, 30);
        _serializedMap = MapSerializer.Serialize(_map);
        
        _entities = new();

        _player = CreatePlayer(_map, _entities, '@');
        _entities.Add(_player); 

        _entities.Add(CreateEnemey(_map, _entities, 'E'));
        _entities.Add(CreateEnemey(_map, _entities, 'E'));   
        */
    }

    public (EntityManager, Map) NewGame()
    {
        var entityManager = new EntityManager();
        var map = new StaticMapGenerator().GenerateMap(30, 30);

        EM.Add(EntityType.Enemy);

        return (entityManager, map);
    }

    public void Run()
    {
        while (true)
        {    
            CombatCheck(_player, _entities);

            Map map = MapSerializer.Deserialize(_serializedMap);

            map.Tiles[_player.PosX, _player.PosY].UpdateEnity(_player.Body);
            map = UpdateEntities(_entities, map);

            var render = new Render(map, _entities, 20);
            render.DrawWorld();

            MovePlayer(map);
        }
    }

    public void RunCombat(Player hero, Enemy enemy)
    {
        var menu = new Dictionary<string, bool>(){
            {"Attack", true},
            {"Defend", false},
            {"Heal", false},
            {"Escape", false}};

        bool action = false;
        

        while (true)
        {
            if (action)
            {

            }

            var render = new RenderCombat(hero, enemy, menu);
            render.Draw();
            
            
            NavigateCombatMenu(menu);
        }
    }

    private void NavigateCombatMenu(Dictionary<string, bool> menu)
    {
        Input input = new Input();

        while (true)
        {
            var inputKey = input.ReadGameInput();

            if (inputKey == InputKey.Up)
            {
                CycleMenu(menu, -1);
                break;
            }
            else if (inputKey == InputKey.Down)
            {
                CycleMenu(menu, 1);
                break;
            }
        }

    }

    private Dictionary<T, bool> CycleMenu<T>(Dictionary<T, bool> menu, int steps) where T : notnull
    {
        var index = menu.Values.ToList().IndexOf(true);

        if (index == -1)
            throw new InvalidOperationException("No item with value 'true' found in the menu.");

        var keys = menu.Keys.ToList();
        menu[keys[index]] = false;
        
        var newIndex = (index + steps) % keys.Count;
        newIndex = (newIndex < 0) ? newIndex + keys.Count : newIndex;
        
        menu[keys[newIndex]] = true;
        
        return menu;
    }



    private void UpdateCombatMenu(Dictionary<string, bool> menu, int v)
    {
        if (v < 0)
        {
            
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
                case InputKey.Esc:
                    Environment.Exit(1);
                    break;
                default:
                    break;

            }
        }
    }

    public void CombatCheck(Player player , List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is Enemy)
            {
                if (!IsAdjacentTiles(player.PosX, player.PosY, entity.PosX, entity.PosY))
                    continue;

                entity.UpdateActive(false);
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

        return (dx <= 1) && (dy <= 1) && (dx + dy > 0);
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

    public bool IsPositionWithinMap(int x, int y, int mapWidth, int mapHeight)
    {
        return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
    }

    private Map UpdateEntities(List<Entity> entities, Map map)
    {
        entities.Where(e => e.IsActive).ToList().ForEach(e => 
                map.Tiles[e.PosX, e.PosY].UpdateEnity(e.Body));
        return map;
    }


    private Player CreatePlayer(Map map, List<Entity> entities, char body)
    {
        var (posX, posY) = GenerateRandomPosition(map);
        var player = new Player(posX, posY);
        player.Body = body;
        return player;
    }

    private Enemy CreateEnemey(Map map, List<Entity> entities, char body)
    {
        var (posX, posY) = GenerateRandomPosition(map);
        var enemy = new Enemy(posX, posY);
        enemy.Body = body;
        return enemy;
    }


    private (int x, int y) GenerateRandomPosition(Map map)
    {
        int posX;
        int posY;

        while (true)
        {
            var random = new Random();
            posX = random.Next(0, map.Width - 1);
            posY = random.Next(0, map.Height - 1);

            if (map.Tiles[posX, posY].Type == TileType.Wall)
                continue;

            if (!ValidEntityPosition(_entities, 2, 2))
                continue;

            break;
        }
        return (posX, posY);
    }


    private bool ValidEntityPosition(List<Entity> entities, int x, int y)
    {
        if (entities.Any(e => e.PosX == x && e.PosY == y))
            return false;
        return true;
    }
}
