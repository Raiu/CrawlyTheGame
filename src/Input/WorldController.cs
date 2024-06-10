namespace Crawly;

class WorldController
{
    private readonly IInputHandler _inputHandler;

    private readonly World _world;

    private readonly bool _isRunning;

    private Player _hero;

    private Dictionary<InputKey, Action> _keyHandlers;

    private Dictionary<InputKey, Action> _registerKeyHandlers => 
        new() {
            {InputKey.Up, PlayerMoveUp},
            {InputKey.Down, PlayerMoveDown},
            {InputKey.Left, PlayerMoveLeft},
            {InputKey.Right, PlayerMoveRight},
            {InputKey.Enter, PlayerInteract},
        };

    ////////////////////////////////////////////////////////////////////////////////
    
    public WorldController(World world, IInputHandler inputHandler)
    {
        _world = world;
        _hero = _world.EntityManager.Hero;
        _inputHandler = inputHandler;
        _inputHandler.RegisterKeyPressHandler(HandleKeyInput);       

        _keyHandlers = _registerKeyHandlers;
    }

    ////////////////////////////////////////////////////////////////////////////////

    private void HandleKeyInput(InputKey key)
    {
        if (!_isRunning || key == InputKey.None)
            return;

        if (_keyHandlers.TryGetValue(key, out var handler))
            handler.Invoke();
    }

    public void PlayerMoveUp()
    {
        throw new NotImplementedException();
    }

    public void PlayerMoveDown()
    {
        throw new NotImplementedException();
    }

    public void PlayerMoveLeft()
    {
        throw new NotImplementedException();
    }

    public void PlayerMoveRight()
    {
        throw new NotImplementedException();
    }

    public void PlayerInteract()
    {
        throw new NotImplementedException();
    }

    public void PlayerOpenInventory()
    {
        throw new NotImplementedException();
    }

    public void MenuCycleUp()
    {
        throw new NotImplementedException();
    }

    public void MenuCycleDown()
    {
        throw new NotImplementedException();
    }

    public void MenuSelect()
    {
        throw new NotImplementedException();
    }

    private void MoveEntity(Entity entity, Coordinate destination)
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

    /*
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
    */

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
    */
}