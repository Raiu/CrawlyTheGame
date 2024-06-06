namespace Crawly;

class Program
{
    static void Main(string[] args)
    {
        IInputHandler inputHandler = new ConsoleInputHandler();
        inputHandler.StartListening();
        // Future implementations
        // IRenderHandler renderHandler = new ConsoleRenderHandler();


        var game = new GameManager(inputHandler);
        game.StartGame();
    }
}
