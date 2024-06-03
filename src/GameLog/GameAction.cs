namespace Crawly;

public abstract class GameAction
{
    public string Actor { get; set; } = "";
    public string Action { get; set; } = "";

    public override string ToString()
    {
        return $"{Actor} {Action}";
    }
}

public class PlayerAction : GameAction
{
    public PlayerAction(string action)
    {
        Actor = "Player";
        Action = action;
    }
}

public class EnemyAction : GameAction
{
    public EnemyAction(string action)
    {
        Actor = "Enemy";
        Action = action;
    }
}

public class WorldAction : GameAction
{
    public WorldAction(string action)
    {
        Actor = "World";
        Action = action;
    }
}