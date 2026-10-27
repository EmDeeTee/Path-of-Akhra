using System.Diagnostics;
using Game;
using Game.Player.Skills;
using Spectre.Console;

namespace Tui.Screens.Combat;

public class CombatScreen(GameSession gameSession, ISkill? skill = null) : IScreen {
    public IScreen Show() {
        if (skill == null) {
            Debug.WriteLine("CombatScreen: Skill selected");
        }
        
        AnsiConsole.Clear();
        AnsiConsole.WriteLine("Combat Screen");
        AnsiConsole.Write(
            new Panel(
                    new BarChart()
                        .Width(40)
                        .AddItem("HP", 90, 100)
                )
                .Header("balright")
                .Expand()
        );
        
        return this;
    }
}