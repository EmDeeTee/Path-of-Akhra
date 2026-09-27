using Game;
using Spectre.Console;
using Tui.Screens.Introduction;

namespace Tui.Screens.MainMenu;

public class MainMenuScreen(GameSession gameSession) : IScreen {
    public IScreen? Show() {
        AnsiConsole.Clear();
        
        FigletText title = new FigletText("Path of Akhra")
            .Centered()
            .Color(Color.Blue);

        AnsiConsole.Write(title);
        AnsiConsole.WriteLine();

        MainMenuChoice option = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuChoice> {
                    PageSize = 5,
                    Converter = choice => choice.ToDisplayName()
                }
                .AddChoices(Enum.GetValues<MainMenuChoice>())
        );

        switch (option) {
            case MainMenuChoice.WalkThePath:
                return new IntroductionScreen(gameSession);
        
            case MainMenuChoice.HallOfFame:
                AnsiConsole.MarkupLine("[grey]The Hall of Fame is not yet implemented.[/]");
                Renderables.PressAnyKey();
                Console.ReadKey(true);
                return this;
            
            case MainMenuChoice.Quit:
                AnsiConsole.MarkupLine("[grey]Goodbye.[/]");
                return null;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, "Unknown main menu choice.");
        }
    }
}