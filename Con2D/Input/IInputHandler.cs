namespace Con2D;

public enum InputKey
{
    None,
    Up,
    Down,
    Left,
    Right,
    Enter,
    Space,
    Esc,
}

public interface IInputHandler
{
    public InputKey ReadGameInput();

    public string ReadKey();

    public string ReadLine();
}
