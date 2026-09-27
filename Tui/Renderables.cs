using Spectre.Console;

namespace Tui;

internal static class Renderables {
    internal static void PressAnyKey() {
        AnsiConsole.Write(new Markup("[gray]Any key to continue...[/]"));
    } 
}