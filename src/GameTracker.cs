namespace Crawly;

public class GameTracker
{
    public event Action<GameState, GameState>? OnGameStateChange;
    public event Action<GameCondition, GameCondition>? OnGameConditionChange;

    private GameState _currentGameState;
    private GameState _previousGameState;
    private GameCondition _currentGameCondition;
    private GameCondition _previousGameCondition;

    public GameState CurrentGameState { 
        get => _currentGameState;
        private set { 
            _currentGameState = value;
            OnGameStateChange?.Invoke(_currentGameState, _previousGameState);
        }
    }

    public GameState PreviousGameState { 
        get => _previousGameState;
        private set => _previousGameState = value;
    }

    public GameCondition CurrentGameCondition { 
        get => _currentGameCondition;
        private set { 
            _currentGameCondition = value;
            OnGameConditionChange?.Invoke(_currentGameCondition, _previousGameCondition);
        }
    }

    public GameCondition PreviousGameCondition { 
        get => _previousGameCondition;
        private set => _previousGameCondition = value;
    }

    ////////////////////////////////////////////////////////////////////////////////

    public GameTracker(GameState gameState) {
        CurrentGameState = gameState;
        PreviousGameState = gameState;
        CurrentGameCondition = GameCondition.None;
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void ChangeGameState(GameState newState) =>
        (PreviousGameState, CurrentGameState) = (CurrentGameState, newState);

    public void ChangeToPreviousGameState() =>
        (PreviousGameState, CurrentGameState) = (CurrentGameState, PreviousGameState);

    public void ChangeGameCondition(GameCondition newCondition) => 
        CurrentGameCondition = newCondition;

}
