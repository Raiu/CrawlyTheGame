namespace Crawly;

class Program
{
    static void Main(string[] args)
    {
        IInputHandler inputHandler = new ConsoleInputHandler();
        // Future implementations
        // IRenderHandler renderHandler = new ConsoleRenderHandler();


        var game = new Game(inputHandler);
        game.StartGame();
    }
}
