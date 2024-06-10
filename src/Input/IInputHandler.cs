
namespace Crawly;

public interface IInputHandler
{
    void RegisterKeyPressHandler(Action<InputKey> handleKeyInput);

    void UnregisterKeyPressHandler(Action<InputKey> handler);

    InputKey ReadInputKey();

    string ReadKey();

    string ReadLine();
}
