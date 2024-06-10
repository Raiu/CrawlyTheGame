
namespace Crawly;

public class ConsoleInputHandler : IInputHandler
{
    private event Action<InputKey>? _onKeyPress;
    private bool _isListening = false;
    private Thread? _listeningThread;
    private static int _threadIdCounter = 0;
    private int _currentThreadId;

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
        { ConsoleKey.D9, InputKey.Nine },
    };

    ////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////

    public void RegisterKeyPressHandler(Action<InputKey> handler)
    {
        if (!IsHandlerRegistered(handler, _onKeyPress))
            return;

        _onKeyPress += handler;
        StartListening();
    }

    public void UnregisterKeyPressHandler(Action<InputKey> handler)
    {
        _onKeyPress -= handler;
        StopListening();
    }

    private void StartListening()
    {
        if (_isListening) return;

        _isListening = true; 
        _currentThreadId = Interlocked.Increment(ref _threadIdCounter);
        _listeningThread = new Thread(ListenInputKey)
        {
            IsBackground = true,
            Name = $"ConsoleInputHandlerThread-{_currentThreadId}"
        };
        _listeningThread.Start();
    }

    private void StopListening()
    {
        if (_onKeyPress != null) return;

        _isListening = false;
        _listeningThread?.Join();
        _listeningThread = null;
    }

    private bool IsHandlerRegistered<T>(Action<T> prospectiveHandler,  Action<T>? reference)
    {
        if (reference != null)
            foreach (Action<T> handler in reference.GetInvocationList().Cast<Action<T>>())
                if (handler == prospectiveHandler) 
                    return true;
        
        return false;
    }

    private void ListenInputKey()
    {
        while (_isListening)
        {
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(50);
                continue;
            }

            var key = Console.ReadKey(true).Key;
            if (_keyMap.TryGetValue(key, out var inputKey))
                _onKeyPress?.Invoke(inputKey);
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
