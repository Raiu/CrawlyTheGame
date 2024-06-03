namespace Con2D;

public enum CombatAction
    {
        None,
        Attack,
        Defend,
        Heal,
        Escape
    }

public class CombatManager
{


    private List<Entity> _entities;
    private Player _hero;
    private Enemy _enemy;

    private CombatAction _actionTaken = CombatAction.None;

    public CombatManager(List<Entity> entities, Player hero, Enemy enemy)
    {
        _entities = entities;
        _hero = hero;
        _enemy = enemy;        
    }

    public void Run()
    {
        var menu = new Dictionary<CombatAction, bool>(){
            {CombatAction.Attack, true},
            {CombatAction.Defend, false},
            {CombatAction.Heal, false},
            {CombatAction.Escape, false},
            };
        
        var won = false;

        var running = true;
        while (running)
        {
            if (_actionTaken != CombatAction.None)
            {
                switch (_actionTaken)
                {
                    case CombatAction.Attack:
                        //_hero.Attack();
                        _enemy.UpdateActive(false);
                        won = true;
                        break;
                    case CombatAction.Defend:
                        //_hero.Defend();
                        _enemy.UpdateActive(false);
                        won = true;
                        break;
                    case CombatAction.Escape:
                        //_hero.Escape();
                        _enemy.UpdateActive(false);
                        won = true;
                        break;
                }
            }

            if (won) break;

            _actionTaken = CombatAction.None;

            var render = new RenderCombat(_hero, _enemy, menu);
            render.Draw();
            
            
            CombatActionMenu(menu);
        }
    }

    private void CombatActionMenu(Dictionary<CombatAction, bool> menu)
    {
        Input input = new Input();

        while (true)
        {
            var inputKey = input.ReadGameInput();

            if (inputKey == InputKey.Up)
            {
                CycleMenu(menu, -1);
                break;
            }
            else if (inputKey == InputKey.Down)
            {
                CycleMenu(menu, 1);
                break;
            }
            else if (inputKey == InputKey.Enter)
            {
                _actionTaken = menu.First(x => x.Value).Key;
                break;
            }
        }

    }


    private void DoAction<T>(T action, Entity actor, Entity target)
    {

    }

    private Dictionary<T, bool> CycleMenu<T>(Dictionary<T, bool> menu, int steps) where T : notnull
    {
        var index = menu.Values.ToList().IndexOf(true);

        if (index == -1)
            throw new InvalidOperationException("No item with value 'true' found in the menu.");

        var keys = menu.Keys.ToList();
        menu[keys[index]] = false;
        
        var newIndex = (index + steps) % keys.Count;
        newIndex = (newIndex < 0) ? newIndex + keys.Count : newIndex;
        
        menu[keys[newIndex]] = true;
        
        return menu;
    }
}
