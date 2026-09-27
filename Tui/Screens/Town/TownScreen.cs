using Game;
using Spectre.Console;
using Tui.Screens.MainMenu;

namespace Tui.Screens.Town;

public class TownScreen(GameSession gameSession) : IScreen {
    public IScreen Show() {
        AnsiConsole.Clear();

        AnsiConsole.Write(
        new Panel(
                new Align(
                    new Markup(gameSession.CurrentSettlement.PrintHeader()),
                    HorizontalAlignment.Center
                )
            )
            .Border(BoxBorder.Double)
            .BorderStyle(new Style(Color.Grey))
            .Padding(2, 1)
        );

        AnsiConsole.WriteLine();

        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]The Path awaits.[/]")
                .PageSize(5)
                .HighlightStyle(
                    new Style(Color.Red, decoration: Decoration.Bold)
                )
                .AddChoices(
                    "Path",
                    "Rest"
                )
        );
        
        return new MainMenuScreen(gameSession);
    }
}