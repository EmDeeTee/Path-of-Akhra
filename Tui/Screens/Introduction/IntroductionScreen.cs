using Game;
using Spectre.Console;
using Tui.Screens.Town;

namespace Tui.Screens.Introduction;

public class IntroductionScreen(GameSession gameSession) : IScreen {
    public void Show() {
        AnsiConsole.Clear();

        DisplayIntroductionBit("[red]Hearken, O Pilgrim...[/]");
        DisplayIntroductionBit("Thou who art bound by the unyielding threads of fate,");
        DisplayIntroductionBit("By blood, by ash, by the last breath of dying gods,");
        DisplayIntroductionBit("Tread now the Path of [blue]Akhra,[/] slayer of [blue]Vaul[/].");
        DisplayIntroductionBit("[red]Fall and rise.[/]");
        
        new TownScreen(gameSession).Show();
    }

    private static void DisplayIntroductionBit(string text) {
        AnsiConsole.Write(new Panel(new Markup(text)));
        Renderables.PressAnyKey();
        ConsoleKeyInfo _ = Console.ReadKey();
        AnsiConsole.Clear();
    }
}