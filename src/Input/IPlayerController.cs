using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawly;

public interface IPlayerController
{

    void PlayerMoveUp();

    void PlayerMoveDown();

    void PlayerMoveLeft();

    void PlayerMoveRight();

    void PlayerInteract();

    void PlayerOpenInventory();

    void MenuCycleUp();

    void MenuCycleDown();

    void MenuSelect();
    
}
