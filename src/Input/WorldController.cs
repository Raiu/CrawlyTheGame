namespace Crawly;

class WorldController : IPlayerController
{
    private readonly IInputHandler _inputHandler;
    
    public WorldController(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

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
                    Environment.Exit(1);
                    break;
                default:
                    break;

            }
        }
    }
    */
}