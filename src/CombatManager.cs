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

    private GameManager _gameManager;
    private CombatInstanceData? _data;

    private Player _hero;
    private Enemy _enemy;

    private CombatAction _actionTaken = CombatAction.None;
    private bool _isRunning;

    public CombatManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _data = _gameManager.CombatInstanceData;

        if (_data != null)
        {
            _hero = _data.Player;
            _enemy = _data.Enemy;
        }
        else
        {
            _gameManager.ChangeToPreviousGameState();
            throw new Exception("no combat data");
        }

        _gameManager.OnGameStateChange += CheckGameState;
        _gameManager.OnGameConditionChange += CheckGameCondition;
        _gameManager.InputHandler.OnKeyPressed += HandleKeyInput;
        
    }

    private Dictionary<CombatAction, bool> _menu;

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
    


    private void DoAction<T>(T action, IEntity actor, IEntity target)
    {

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

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        throw new NotImplementedException();
    }

    private void CheckGameState() {
        if (_gameManager.CurrentGameState != GameState.Combat) _isRunning = false;
    }

    private void CheckGameCondition() {
        if (_gameManager.CurrentGameCondition != GameCondition.None) _isRunning = false;
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
