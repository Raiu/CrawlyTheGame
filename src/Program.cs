namespace Crawly;

class Program
{
    static void Main(string[] args)
    {
        // Future implementations
        // IRenderHandler renderHandler = new ConsoleRenderHandler();
        IInputHandler inputHandler = new ConsoleInputHandler();


        var game = new Game(inputHandler);
        game.StartGame();
    }
}
