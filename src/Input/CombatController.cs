namespace Crawly;

class CombatController
{
    private readonly IInputHandler _inputHandler;

    private readonly bool _isRunning;

    private Dictionary<InputKey, Action> _keyHandlers;

    ////////////////////////////////////////////////////////////////////////////////
    
    public CombatController(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.RegisterKeyPressHandler(HandleKeyInput);

        _keyHandlers = _registerKeyHandlers;

    }

    ////////////////////////////////////////////////////////////////////////////////

    private Dictionary<InputKey, Action> _registerKeyHandlers => new() {
        { InputKey.Up, PlayerMoveUp },
        { InputKey.Down, PlayerMoveDown },
        { InputKey.Left, PlayerMoveLeft },
        { InputKey.Right, PlayerMoveRight },
        { InputKey.Enter, PlayerInteract },
    };

    private void HandleKeyInput(InputKey key)
    {
        if (!_isRunning || key == InputKey.None)
            return;

        if (_keyHandlers.TryGetValue(key, out var handler))
            handler.Invoke();
    }

    private void CombatMenuCycleUp()
    {
        throw new NotImplementedException();
    }

    private void CombatMenuCycleDown()
    {
        throw new NotImplementedException();
    }

    private void CombatMenuSelect()
    {
        throw new NotImplementedException();
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
}