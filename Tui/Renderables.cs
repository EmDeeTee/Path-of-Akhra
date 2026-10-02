using Game;
using Spectre.Console;

namespace Tui;

internal static class Renderables {
    internal static void PressAnyKey() {
        AnsiConsole.Write(new Markup("[gray]Any key to continue...[/]"));
    }

    internal static void ShowPlayerPanel(Player player) {
        AnsiConsole.Write(
            new Panel(
                    new Align(
                        new Markup($"[red]{player.Health}[/]/[gray]{player.MaxHealth}[/] | [yellow]{player.Gold} Gold[/]"),
                        HorizontalAlignment.Center
                    )
                )
                .Header("You")
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.Yellow))
                .Padding(2, 1)
        );
    }
}