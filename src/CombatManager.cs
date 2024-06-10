using System.ComponentModel.Design;

namespace Crawly;

public enum CombatAction
{
    None,
    Attack,
    Defend,
    Heal,
    Escape
}

public class CombatManager : IGameStateManager
{
    private Game _gameManager;
    private GameTracker _gameTracker;

    private GameState _currentGameState;
    private GameCondition _currentGameCondition;

    private CombatInstanceData? _data;

    private Player _hero;
    private Enemy _enemy;

    private CombatAction _actionTaken = CombatAction.None;
    private bool _isRunning;

    private Dictionary<CombatAction, bool> _menu;

    ////////////////////////////////////////////////////////////////////////////////

    public CombatManager(Game gameManager)
    {
        _gameManager = gameManager;
        _gameTracker = _gameManager.GameTracker;

        _currentGameState = _gameTracker.CurrentGameState;
        _currentGameCondition = _gameTracker.CurrentGameCondition;

        if (_gameManager.CombatInstanceData != null)
        {
            _data = _gameManager.CombatInstanceData;
            _hero = _data.Player;
            _enemy = _data.Enemy;
        }
        else
        {
            _gameTracker.ChangeToPreviousGameState();
            throw new Exception("no combat data");
        }

        _gameTracker.OnGameStateChange += CheckGameState;
        _gameTracker.OnGameConditionChange += CheckGameCondition;        
    }

    ////////////////////////////////////////////////////////////////////////////////

    public void Run()
    {
        _menu = new Dictionary<CombatAction, bool>(){
                {CombatAction.Attack, true},
                {CombatAction.Defend, false},
                {CombatAction.Heal, false},
                {CombatAction.Escape, false},
            };

        var won = false;

        _isRunning = true;
        while (_isRunning)
        {
            if (_actionTaken != CombatAction.None)
            {
                switch (_actionTaken)
                {
                    case CombatAction.Attack:
                        //_hero.Attack();
                        _enemy.SetIsActive(false);
                        won = true;
                        break;
                    case CombatAction.Defend:
                        //_hero.Defend();
                        _enemy.SetIsActive(false);
                        won = true;
                        break;
                    case CombatAction.Escape:
                        //_hero.Escape();
                        _enemy.SetIsActive(false);
                        won = true;
                        break;
                }
            }

            if (won) break;

            _actionTaken = CombatAction.None;

            var render = new RenderCombat(_hero, _enemy, _menu);
            render.Draw();


            //HandleKeyInput(menu);

            Thread.Sleep(2000);
        }
    }

    private void HandleKeyInput(InputKey key)
    {
        if (key == InputKey.None)
            return;

        var menu = _menu;

        switch (key)
        {
            case InputKey.Up:
                menu = CycleMenu(menu, -1);
                break;
            case InputKey.Down:
                menu = CycleMenu(menu, 1);
                break;
            case InputKey.Enter:
                _actionTaken = menu.First(x => x.Value).Key;
                break;
        }
    }
    
    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        throw new NotImplementedException();
    }

    private void GameStateChangeHandler(GameState newGameState, GameState oldGameState)
    {

    }

    private void GameConditionChangeHandler(GameCondition newGameCondition, GameCondition oldGameCondition)
    {

    }

    private void CheckGameState() {
        if (_gameManager._currentGameState != GameState.Combat) _isRunning = false;
    }

    private void CheckGameCondition() {
        if (_gameManager._currentGameCondition != GameCondition.None) _isRunning = false;
    }

    void IGameStateManager.CheckGameState()
    {
        throw new NotImplementedException();
    }

    void IGameStateManager.CheckGameCondition()
    {
        throw new NotImplementedException();
    }

    /* Graveyard
    
    private void CombatActionMenu(Dictionary<CombatAction, bool> menu)
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
            else if (inputKey == InputKey.Enter)
            {
                _actionTaken = menu.First(x => x.Value).Key;
                break;
            }
        }

    }

    */
}
