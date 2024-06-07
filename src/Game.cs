namespace Crawly;

public class Game
{
    private bool _isRunning;
    private GameCondition _currentGameCondition;
    private GameState _currentGameState;

    public event Action? OnGameStateChange;
    public event Action? OnGameConditionChange;

    public GameState CurrentGameState
    { 
        get => _currentGameState;
        private set
        { 
            _currentGameState = value;
            OnGameStateChange?.Invoke();
        }
    }

    public GameState PreviousGameState { get; private set; }

    public GameCondition CurrentGameCondition
    { 
        get => _currentGameCondition;
        private set
        { 
            _currentGameCondition = value;
            OnGameConditionChange?.Invoke();
        }
    }

    public IInputHandler InputHandler { get; private set; }

    public World World { get; private set; } = null!;

    public EntityManager EntityManager { get; private set; } = null!;

    public CombatInstanceData? CombatInstanceData { get; set; }

    public Game(IInputHandler inputHandler)
    {
        InputHandler = inputHandler;

        CurrentGameState = GameState.MainMenu;
        PreviousGameState = GameState.None;
        CurrentGameCondition = GameCondition.None;

        StartGame();
    }

    public void StartGame()
    {
        _isRunning = true;
        while (_isRunning)
        {
            switch (CurrentGameState)
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

    // OnGameStateChange?.Invoke();
    public void ChangeGameState(GameState newState) => CurrentGameState = newState;

    // OnGameStateChange?.Invoke();
    public void ChangeToPreviousGameState() => CurrentGameState = PreviousGameState;

    // OnGameConditionChange?.Invoke();
    public void ChangeGameCondition(GameCondition newCondition) => CurrentGameCondition = newCondition;

    private void CreateNewGame()
    {
        if (CheckManagerInitiated()) return;

        NewGame(out Map map, out EntityManager em);
        EntityManager = em;
        World = new(this, map);
        CurrentGameState = GameState.World;
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
