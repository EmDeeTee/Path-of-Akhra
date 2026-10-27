using Game;
using Game.Player.Skills;
using Spectre.Console;
using Tui.Screens.Combat;

namespace Tui.Screens.SkillSelect;

public class SkillSelectScreen(GameSession gameSession) : IScreen {
    public IScreen Show() {
        AnsiConsole.Clear();

        ISkill choice = AnsiConsole.Prompt(new SelectionPrompt<ISkill>(
            ).Title("Skills")
            .UseConverter(skill => skill.GetName(gameSession))
            .AddChoices(gameSession.Player.Skills)
        );

        return new CombatScreen(gameSession, choice);
    }
}