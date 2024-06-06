
namespace Crawly;

public class ConsoleInputHandler : IInputHandler
{
    // public delegate void OnKeyPressedHandler(InputKey inputKey);

    // public event OnKeyPressedHandler? OnKeyPressed;
    
    public event Action<InputKey>? OnKeyPressed;

    private readonly Dictionary<ConsoleKey, InputKey> _keyMap = new()
    {
        { ConsoleKey.UpArrow, InputKey.Up },
        { ConsoleKey.DownArrow, InputKey.Down },
        { ConsoleKey.LeftArrow, InputKey.Left },
        { ConsoleKey.RightArrow, InputKey.Right },
        { ConsoleKey.Enter, InputKey.Enter },
        { ConsoleKey.Spacebar, InputKey.Space },
        { ConsoleKey.Escape, InputKey.Esc },
        { ConsoleKey.Tab, InputKey.Tab },
        { ConsoleKey.D0, InputKey.Zero },
        { ConsoleKey.D1, InputKey.One },
        { ConsoleKey.D2, InputKey.Two },
        { ConsoleKey.D3, InputKey.Three },
        { ConsoleKey.D4, InputKey.Four },
        { ConsoleKey.D5, InputKey.Five },
        { ConsoleKey.D6, InputKey.Six },
        { ConsoleKey.D7, InputKey.Seven },
        { ConsoleKey.D8, InputKey.Eight },
        { ConsoleKey.D9, InputKey.Nine }
    };

    public void StartListening() {
        var thread = new Thread(ListenInputKey)
        {
            IsBackground = true
        };
        thread.Start();
    }

    public void ListenInputKey()
    {
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(50);
                continue;
            }

            var key = Console.ReadKey(true).Key;
            if (_keyMap.TryGetValue(key, out var inputKey))
                OnKeyPressed?.Invoke(inputKey);
        }
    }

    public InputKey ReadInputKey()
    {
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(50);
                continue;
            }

            var key = Console.ReadKey(true).Key;
            if (_keyMap.TryGetValue(key, out var inputKey))
                return inputKey;
        }
    }

    public string ReadKey() => Console.ReadKey(true).Key.ToString();

    public string ReadLine() => Console.ReadLine() ?? "";

    
}
