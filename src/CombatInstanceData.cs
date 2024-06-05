namespace Crawly;

public enum CombatCondition
{
    None,
    Won,
    Lost
}

public class CombatInstanceData
{
    public CombatCondition Result { get; set; } = CombatCondition.None;
    public Player Player { get; set; }
    public Enemy Enemy { get; set; }

    public List<CombatAction> ActionLog { get; set; } = new List<CombatAction>();

    public CombatInstanceData(Player player, Enemy enemy)
    {
        Player = player;
        Enemy = enemy;
    }
}
