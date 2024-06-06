namespace Crawly;

class CombatController : IPlayerController
{
    private readonly IInputHandler _inputHandler;
    
    public CombatController(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }
}