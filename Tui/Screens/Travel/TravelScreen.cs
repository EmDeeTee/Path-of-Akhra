using Game;
using Game.Events;
using Game.Settlements;
using Spectre.Console;
using Tui.Screens.Town;

namespace Tui.Screens.Travel;

public class TravelScreen(GameSession gameSession) : IScreen {
    public IScreen Show() {
        AnsiConsole.Clear();

        ShowDeparturePanel();

        if (gameSession.CurrentEventSequence is { Length: > 0 }) {
            foreach (IEvent gameEvent in gameSession.CurrentEventSequence) {
                EventResult result = gameEvent.Execute(gameSession);
                gameSession.ApplyEventResult(result);
                DisplayEvent(result);
            }
        }

        ShowArrivalPanel();

        return new TownScreen(gameSession);
    }

    private void ShowDeparturePanel() {
        Settlement from = gameSession.CurrentSettlement;
        Settlement to = gameSession.TravelTargetSettlement!;

        AnsiConsole.Write(
            new Panel(
                    new Align(
                        new Markup(
                            $"You leave [grey]{from.Name}[/] behind and set out along the Path toward [blue]{to.Name}[/]."
                        ),
                        HorizontalAlignment.Center
                    )
                )
                .Header("Departure")
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Blue))
                .Padding(2, 1)
        );
        Renderables.PressAnyKey();
        Console.ReadKey();
    }

    private void DisplayEvent(EventResult result) {
        AnsiConsole.Clear();

        AnsiConsole.Write(
            new Panel(new Markup(result.Description))
                .Header("On the Path")
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Grey))
                .Padding(2, 1)
        );

        if (result.FoundGold > 0) {
            AnsiConsole.MarkupLine($"[yellow]{result.FoundGold}[/] gold gained.");
        }

        if (result.HealedHealth > 0) {
            AnsiConsole.MarkupLine($"[red]{result.HealedHealth}[/] health recovered.");
        }

        Renderables.PressAnyKey();
        Console.ReadKey();
    }

    private void ShowArrivalPanel() {
        gameSession.CompleteTravel();
        Settlement settlement = gameSession.CurrentSettlement;

        AnsiConsole.Clear();

        AnsiConsole.Write(
            new Panel(
                    new Align(
                        new Markup(settlement.PrintHeader()),
                        HorizontalAlignment.Center
                    )
                )
                .Header($"You arrive at {settlement.Name}")
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Blue))
                .Padding(2, 1)
        );
        Renderables.PressAnyKey();
        Console.ReadKey();
    }
}