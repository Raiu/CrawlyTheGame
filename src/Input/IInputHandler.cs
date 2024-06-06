
namespace Crawly;

public interface IInputHandler
{
    event Action<InputKey> OnKeyPressed;

    InputKey ReadInputKey();

    string ReadKey();

    string ReadLine();

    void StartListening();
}
