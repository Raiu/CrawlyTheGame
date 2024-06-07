namespace Crawly;

public class WorldManager : IGameStateManager
{
    private GameManager _gameManager;

    private readonly WorldController _worldController;

    private JsonMapSerializer MapSerializer = new JsonMapSerializer();
    private Map _map;
    private string _mapRenderBase;

    private EntityManager _entityManager;
    private Player _hero;

    private GameCondition _gameCondition;
    private GameState _gameState;

    private bool _isRunning;

    public WorldManager(GameManager gameManager, Map map)
    {
        _gameManager = gameManager;
        _worldController = new WorldController(_gameManager.InputHandler);

        _entityManager = _gameManager.EntityManager;
        _hero = _entityManager.Hero;

        _map = map;
        _mapRenderBase = MapSerializer.Serialize(_map);

        _gameManager.OnGameStateChange += CheckGameState;
        _gameManager.OnGameConditionChange += CheckGameCondition;

        _gameManager.InputHandler.OnKeyPressed += HandleKeyInput;

        RegisterKeyHandler(HandleKeyInput);
    }

    public void Run()
    {
        _isRunning = true;
        while (_isRunning)
        {
            CombatCheck(_hero, _entityManager.Active);


            if (!_isRunning) break;

            var map = new JsonMapSerializer().Deserialize(_mapRenderBase);
            map = UpdateTilesWithEntities(_entityManager.Drawables, map);

            var render = new RenderWorld(map, _entityManager.Active, 20);
            render.Draw();

            // MovePlayer(map);

            Thread.Sleep(2000);
        }
    }

    private void RegisterKeyHandler(Action<InputKey> handler)
    {
        if (!_gameManager.InputHandler.IsHandlerRegistered(handler))
        {
            _gameManager.InputHandler.OnKeyPressed += handler;
        }
    }

    private void HandleKeyInput(InputKey key)
    {
        if (!_isRunning) return;

        if (key == InputKey.None)
            return;

        switch (key)
        {
            case InputKey.Up:
                if (!ValidMove(new Coordinate(_hero.Position.X, _hero.Position.Y - 1), _map, _entityManager.Active))
                    break;
                _hero.Move(new Coordinate(_hero.Position.X, _hero.Position.Y - 1));
                return;
            case InputKey.Down:
                if (!ValidMove(new Coordinate(_hero.Position.X, _hero.Position.Y + 1), _map, _entityManager.Active))
                    break;
                _hero.Move(new Coordinate(_hero.Position.X, _hero.Position.Y + 1));
                return;
            case InputKey.Left:
                if (!ValidMove(new Coordinate(_hero.Position.X - 1, _hero.Position.Y), _map, _entityManager.Active))
                    break;
                _hero.Move(new Coordinate(_hero.Position.X - 1, _hero.Position.Y));
                return;
            case InputKey.Right:
                if (!ValidMove(new Coordinate(_hero.Position.X + 1, _hero.Position.Y), _map, _entityManager.Active))
                    break;
                _hero.Move(new Coordinate(_hero.Position.X + 1, _hero.Position.Y));
                return;
            case InputKey.Esc:
                Environment.Exit(0);
                break;
            default:
                break;

        }
    }

    private void CheckGameState() {
        if (_gameManager.CurrentGameState != GameState.World) _isRunning = false;
    }

    private void CheckGameCondition() {
        if (_gameManager.CurrentGameCondition != GameCondition.None) _isRunning = false;
    }

    

    public bool CombatCheck(Player player, List<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is Enemy enemy)
            {
                if (!IsAdjacentTiles(player.Position.X, player.Position.Y, 
                                     enemy.Position.X, enemy.Position.Y))
                    continue;

                _gameManager.CombatInstanceData = new CombatInstanceData(player, enemy);
                _gameState = GameState.Combat;
                return true;
            }

        }
        return false;
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

    public bool ValidMove(Coordinate position, Map map, List<IEntity> entities)
    {
        if (!IsPositionWithinMap(position.X, position.Y, map.Width, map.Height))
            return false;

        if (map.Tiles[position.X, position.Y].Type == TileType.Wall)
            return false;

        if (entities.Any(e => e.Position == position && e.IsActive))
            return false;

        return true;
    }

    private Map UpdateTilesWithEntities(List<IDrawable> entities, Map map)
    {
        entities.Where(e => e.IsVisible).ToList().ForEach(e =>
                map.Tiles[e.Position.X, e.Position.Y].UpdateEnity(e.Body));
        return map;
    }

    public bool IsPositionWithinMap(int x, int y, int mapWidth, int mapHeight) =>
            x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;


    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        throw new NotImplementedException();
    }

    void IGameStateManager.CheckGameState()
    {
        throw new NotImplementedException();
    }

    void IGameStateManager.CheckGameCondition()
    {
        throw new NotImplementedException();
    }

    // Graveyard
    /*
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
                    if (!ValidMove(new Coordinate(_hero.Position.X, _hero.Position.Y - 1), map, _entityManager.Active))
                        break;
                    _hero.Move(new Coordinate(_hero.Position.X, _hero.Position.Y - 1));
                    return;
                case InputKey.Down:
                    if (!ValidMove(new Coordinate(_hero.Position.X, _hero.Position.Y + 1), map, _entityManager.Active))
                        break;
                    _hero.Move(new Coordinate(_hero.Position.X, _hero.Position.Y + 1));
                    return;
                case InputKey.Left:
                    if (!ValidMove(new Coordinate(_hero.Position.X - 1, _hero.Position.Y), map, _entityManager.Active))
                        break;
                    _hero.Move(new Coordinate(_hero.Position.X - 1, _hero.Position.Y));
                    return;
                case InputKey.Right:
                    if (!ValidMove(new Coordinate(_hero.Position.X + 1, _hero.Position.Y), map, _entityManager.Active))
                        break;
                    _hero.Move(new Coordinate(_hero.Position.X + 1, _hero.Position.Y));
                    return;
                case InputKey.Esc:
                    Environment.Exit(0);
                    break;
                default:
                    break;

            }
        }
    }

    private bool ValidEntityPosition(List<IEntity> entities, int posX, int posY) =>
            !entities.Any(e => e.PosX == posX && e.PosY == posY);

    */
}
