using System.Text;
using Spectre.Console;

namespace Crawly;

public class RenderCombat : IRender
{
    private Player _player;
    private Enemy _enemy;
    private CombatLog _log = new();
    private Dictionary<CombatAction, bool> _ActionMenu;

    public RenderCombat(Player player, Enemy enemy, Dictionary<CombatAction, bool> menu)
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

        layout["Left"].Update(
            new Panel(
                Align.Center(
                    new Panel(new Text($"{EntityModels.hero}")).NoBorder()))
                .Expand()
            );

        layout["right"].Update(
            new Panel(
                Align.Center(
                    new Panel(new Text($"{EntityModels.skeleton}")).NoBorder()))
                .Expand()
            );

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
