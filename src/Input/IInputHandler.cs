
namespace Crawly;

public interface IInputHandler
{
    event Action<InputKey> OnKeyPressed;

    bool IsHandlerRegistered(Action<InputKey> handleKeyInput);

    InputKey ReadInputKey();

    string ReadKey();

    string ReadLine();

    // void StartListening();
}
