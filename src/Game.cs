namespace Crawly;

public class Game
{
    private bool _isRunning;
    private GameCondition _currentGameCondition;
    private GameState _previousGameState;
    private GameState _currentGameState;

    public GameTracker GameTracker { get; private set; }

    public IInputHandler InputHandler { get; private set; }

    public World World { get; private set; } = null!;

    public EntityManager EntityManager { get; private set; } = null!;

    public CombatInstanceData? CombatInstanceData { get; set; }

    ////////////////////////////////////////////////////////////////////////////////

    public Game(IInputHandler inputHandler)
    {
        GameTracker = new GameTracker(GameState.MainMenu);
        _currentGameState = GameTracker.CurrentGameState;
        _previousGameState = GameTracker.PreviousGameState;
        _currentGameCondition = GameTracker.CurrentGameCondition;

        InputHandler = inputHandler;

        StartGame();
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void StartGame()
    {
        _isRunning = true;
        while (_isRunning)
        {
            switch (_currentGameState)
            {
                case GameState.MainMenu:
                    // Placeholder starter
                    CreateNewGame();
                    break;
                case GameState.InGameMenu:
                    break;
                case GameState.World:
                    if (CheckManagerInitiated()) World.Run();
                    break;
                case GameState.Combat:
                    var combatInstance = new CombatManager(this);
                    combatInstance.Run();                    
                    break;
                default:
                    break;
            }
        }
    }

    public void Update()
    {

    }

    public void Render()
    {
        
    }

    private void CreateNewGame()
    {
        if (CheckManagerInitiated()) return;

        NewGame(out Map map, out EntityManager em);
        EntityManager = em;
        World = new(this, map);
        _currentGameState = GameState.World;
    }

    private void NewGame(out Map map, out EntityManager em)
    {
        map = new StaticMapGenerator().GenerateMap(30, 30);

        em = new EntityManager(map);
        em.Add(EntityType.Enemy);
    }

    private bool CheckManagerInitiated() =>
        World != null && EntityManager != null;

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

    private void WinScreen()
    {
        Console.Clear();
        Console.WriteLine("YOU WON");

        Console.ReadKey(true);

        Environment.Exit(0);
    }
}
