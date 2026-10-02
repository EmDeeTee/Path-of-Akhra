using Game;
using Game.Settlements;
using Spectre.Console;
using Tui.Screens.MainMenu;
using Tui.Screens.Travel;

namespace Tui.Screens.Town;

public class TownScreen(GameSession gameSession) : IScreen {
    public IScreen Show() {
        AnsiConsole.Clear();

        ShowSettlementPanel();
        Renderables.ShowPlayerPanel(gameSession.Player);

        AnsiConsole.WriteLine();

        TownChoice choice = AnsiConsole.Prompt(
            new SelectionPrompt<TownChoice> {
                    Converter = static choice => choice.ToDisplayName()
                }
                .Title("[red]The Path awaits.[/]")
                .PageSize(5)
                .HighlightStyle(
                    new Style(Color.Red, decoration: Decoration.Bold)
                )
                .AddChoices(Enum.GetValues<TownChoice>())
        );

        switch (choice) {
            case TownChoice.ContinueThePath:
                gameSession.TravelTo(Settlement.Next());
                return new TravelScreen(gameSession);

            case TownChoice.Rest:
                Rest();
                return this;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(choice), choice, "Unknown choice");
        }
    }

    private void ShowSettlementPanel() {
        AnsiConsole.Write(
            new Panel(
                    new Align(
                        new Markup(gameSession.CurrentSettlement.PrintHeader()),
                        HorizontalAlignment.Center
                    )
                )
                .Header("Settlement")
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Grey))
                .Padding(2, 1)
        );
    }

    private void Rest() {
        AnsiConsole.Clear();
        
        if (gameSession.CurrentSettlement.HasPlayerRested) {
            AnsiConsole.Write("You have already rested at this settlement.");
            Console.ReadKey();
            return;
        }
        
        int recoveredHealth = gameSession.Player.Heal(100);
        gameSession.CurrentSettlement.HasPlayerRested = true;

        AnsiConsole.Write(recoveredHealth <= 0
            ? new Markup("You sleep, but you were already fully rested.")
            : new Markup($"You sleep and recover [red]{recoveredHealth}[/] health."));
        Console.ReadKey();
    }
}