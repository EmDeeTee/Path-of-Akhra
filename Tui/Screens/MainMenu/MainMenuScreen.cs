using Game;
using Spectre.Console;
using Tui.Screens.Introduction;

namespace Tui.Screens.MainMenu;

public class MainMenuScreen(GameSession gameSession) : IScreen {
    public void Show() {
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
                new IntroductionScreen(gameSession).Show();
                break;
        
            case MainMenuChoice.HallOfFame:
                throw new NotImplementedException("Hall of Fame is not yet implemented");
                break;
            
            case MainMenuChoice.Quit:
                AnsiConsole.MarkupLine("[grey]Goodbye.[/]");
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}