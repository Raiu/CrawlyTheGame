using System.Text;
using Spectre.Console;

namespace Con2D;

public class RenderCombat : IRender
{
    private Player _player;
    private Enemy _enemy;
    private CombatLog _log = new();
    private Dictionary<string, bool> _ActionMenu;

    public RenderCombat(Player player, Enemy enemy, Dictionary<string, bool> menu)
    {
        _player = player;
        _enemy = enemy;
        _ActionMenu = menu;
    }

    public void Draw()
    {
        AnsiConsole.Clear();

        var layout = GenMain();

        var actionMenu = new Markup(GenActionMenu());
        var actionMenuPanel = new Panel(actionMenu) { Width = 16 };

        layout["Left"].Update(Align.Right(new Panel("test left")));
        layout["right"].Update(new Panel("test right"));

        layout["ActionMenu"].Update(Align.Center(actionMenuPanel));
        layout["CombatLog"].Update(new Panel("Combat Log").Expand());

        AnsiConsole.Write(layout);
    }

    private string GenActionMenu()
    {
        var sb = new StringBuilder();
        foreach (var action in _ActionMenu)
        {
            if (action.Value == true)
                sb.Append($"[bold]|>{action.Key}[/]");
            else
                sb.Append($"  {action.Key}");
            sb.Append('\n');
        }
        return sb.ToString();
    }

    private Layout GenMain()
    {
        // var layout = new Layout("Root")
        //     .SplitColumns(
        //         new Layout("Left"),
        //         new Layout("Right"));

        /*
        layout["Left"].Update(
            new Panel(Align.Center(
                    new Markup("Test left"))));
        layout["Right"].Update( new Panel( Align.Center( new Markup("test right") ) ) );
        */

        return new Layout("Root")
            .SplitRows(
                new Layout("Top").SplitColumns(
                    new Layout("Left"),
                    new Layout("Right")
                ).Ratio(4),
                new Layout("Bottom").SplitColumns(
                    new Layout("ActionMenu").Ratio(3),
                    new Layout("CombatLog").Ratio(4)
                ).Ratio(2)
            );
    }
}
