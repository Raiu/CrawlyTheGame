namespace Con2D;

public enum CombatAction
{
    Attack,
    Defend,
    Escape
}

public interface ICombat
{
    public void Attack();

    public void Defend();

    public void Escape();

    public List<CombatAction> GetActions(); 
}
