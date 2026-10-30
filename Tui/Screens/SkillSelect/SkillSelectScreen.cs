using System.Diagnostics;
using Game;
using Game.Player.Skills;
using Spectre.Console;
using Tui.Screens.Combat;

namespace Tui.Screens.SkillSelect;

public class SkillSelectScreen(GameSession gameSession) : IScreen {
    public IScreen Show() {
        AnsiConsole.Clear();

        List<ISkill> skills = gameSession.Player.Skills.ToList();
        int selectedIndex = 0;

        ISkill choice = AnsiConsole.Live(
            BuildSkillSelection(skills, selectedIndex)
        ).Start(context => {
            while (true) {
                context.UpdateTarget(
                    BuildSkillSelection(skills, selectedIndex)
                );

                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (key) {
                    case ConsoleKey.UpArrow:
                        selectedIndex =
                            (selectedIndex - 1 + skills.Count) % skills.Count;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex =
                            (selectedIndex + 1) % skills.Count;
                        break;

                    case ConsoleKey.Enter:
                        return skills[selectedIndex];

                    default:
                        continue;
                }
            }
        });

        return new CombatScreen(gameSession, choice);
    }

    private Columns BuildSkillSelection(List<ISkill> skills, int selectedIndex) {
        Debug.WriteLine("Building Skill Selection");
        string choices = string.Join(
            Environment.NewLine,
            skills.Select((skill, index) => {
                string marker = index == selectedIndex
                    ? "[bold yellow]>[/]"
                    : " ";

                return $"{marker} {Markup.Escape(skill.GetName(gameSession))}";
            })
        );

        var skillsPanel = new Panel(new Markup(choices))
            .Header("Skills").Expand();

        ISkill selectedSkill = skills[selectedIndex];

        string damage = selectedSkill
            .GetDamage(gameSession)
            .ToString();

        Panel previewPanel = new Panel(
            new Markup(
                $"[bold]{Markup.Escape(selectedSkill.GetName(gameSession))}[/]" +
                $"\n\nDamage: [red]{Markup.Escape(damage)}[/]"
            )
        ).Header("Selected Skill");

        return new Columns(skillsPanel, previewPanel);
    }
}