namespace Crawly;

public class World : IGameStateManager
{
    private Game _game;
    
    private GameTracker _gameTracker;

    private readonly WorldController _worldController;

    private JsonMapSerializer MapSerializer = new JsonMapSerializer();
    
    private Map _map;
    
    private string _mapRenderBase;

    public EntityManager EntityManager { get; private set; }
   
    private Player _hero;

    private GameCondition _gameCondition;
   
    private GameState _gameState;

    private bool _isRunning;

    ////////////////////////////////////////////////////////////////////////////////

    public World(Game gameManager, Map map)
    {
        _game = gameManager;
        _gameTracker = _game.GameTracker;

        _worldController = new WorldController(this, _game.InputHandler);

        EntityManager = _game.EntityManager;
        _hero = EntityManager.Hero;

        _map = map;
        _mapRenderBase = MapSerializer.Serialize(_map);

        _gameTracker.OnGameStateChange += GameStateChangeHandler;
        _gameTracker.OnGameConditionChange += GameConditionChangeHandler;
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void Run()
    {
        _isRunning = true;
        while (_isRunning)
        {
            CombatCheck(_hero, EntityManager.Active);


            if (!_isRunning) break;

            

            var render = new RenderWorld(_map, EntityManager.Active, 20);
            render.Draw();

            // MovePlayer(map);

            Thread.Sleep(100);
        }
    }

    private void GameStateChangeHandler(GameState newState, GameState oldState)
    {
        if (_game._currentGameState != GameState.World) _isRunning = false;

    }

    private void GameConditionChangeHandler(GameCondition newCondition, GameCondition oldCondition)
    {
        if (_game._currentGameCondition != GameCondition.None) _isRunning = false;
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

                _game.CombatInstanceData = new CombatInstanceData(player, enemy);
                _gameState = GameState.Combat;
                return true;
            }

        }
        return false;
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

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        throw new NotImplementedException();
    }

    // Graveyard
    /*
    
    */
}
