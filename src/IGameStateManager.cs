using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawly;

public interface IGameStateManager
{
    public void Run();

    public void Update();

    public void Render();

    public void CheckGameState();

    public void CheckGameCondition();
    
}
