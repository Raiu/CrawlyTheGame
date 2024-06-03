namespace Crawly;

public class Input : IInputHandler
{

    public InputKey ReadGameInput()
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    return InputKey.Up;
                case ConsoleKey.DownArrow:
                    return InputKey.Down;
                case ConsoleKey.LeftArrow:
                    return InputKey.Left;
                case ConsoleKey.RightArrow:
                    return InputKey.Right;
                case ConsoleKey.Enter:
                    return InputKey.Enter;
                case ConsoleKey.Spacebar:
                    return InputKey.Space;
                case ConsoleKey.Escape:
                    return InputKey.Esc;
                default:
                    return InputKey.None;
            }
        }
    }

    public string ReadKey() => Console.ReadKey(true).Key.ToString();

    public string ReadLine() => Console.ReadLine() ?? "";
}
