using System.Text;
using Game;
using Spectre.Console;
using Tui.Screens.Introduction;

namespace Tui.Screens.MainMenu;

public class MainMenuScreen(GameSession gameSession) : IScreen {
    private static readonly string[][] TorchFrames = [
        ["     .     ", "    ( )    ", "   (   )   ", "   (___)   "],
        ["    .*.    ", "   (   )   ", "  (     )  ", "   (___)   "],
        ["     *     ", "    ( )    ", "   (   )   ", "   (___)   "],
        ["    .      ", "   (   )   ", "  (     )  ", "   (___)   "]
    ];

    private static readonly string[] TorchHardware = [
        "     |     ",
        "    /|\\    ",
        "   / | \\   ",
        "  |=====|  ",
        "     |     ",
        "   _/ \\_   "
    ];

    public IScreen? Show() {
        AnsiConsole.Clear();

        ShowAnimatedTitle();
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
    
    private static void ShowAnimatedTitle() {
        AnsiConsole.Live(GetTitleGrid(TorchFrames[0], TorchFrames[2]))
            .Start(context => {
                const int frameDelayMs = 120;
                const int maxFrames = 112;

                for (int frame = 0; frame < maxFrames; frame++) {
                    if (Console.KeyAvailable) {
                        break;
                    }

                    string[] leftFlame = TorchFrames[frame % TorchFrames.Length];
                    string[] rightFlame = TorchFrames[(frame + 2) % TorchFrames.Length];
                    context.UpdateTarget(GetTitleGrid(leftFlame, rightFlame));
                    context.Refresh();
                    Thread.Sleep(frameDelayMs);
                }
            });
    }
    
    private static Grid GetTitleGrid(string[] leftFlame, string[] rightFlame) {
        Markup title = new("[bold yellow]P A T H   O F   A K H R A[/]");

        Panel titleCard = new Panel(
            new Align(title, HorizontalAlignment.Center)
        )
        .Border(BoxBorder.Double)
        .BorderStyle(new Style(Color.Yellow))
        .Padding(2, 1)
        .Expand();

        Grid grid = new();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow(
            new Align(BuildTorch(leftFlame), HorizontalAlignment.Left, VerticalAlignment.Middle),
            new Align(titleCard, HorizontalAlignment.Center, VerticalAlignment.Middle),
            new Align(BuildTorch(rightFlame), HorizontalAlignment.Right, VerticalAlignment.Middle)
        );
        return grid.Expand();
    }
    
    private static Markup BuildTorch(string[] flame) {
        StringBuilder builder = new();
        string[] lines = [.. flame, .. TorchHardware];

        for (int i = 0; i < lines.Length; i++) {
            string color = i switch {
                0 => "yellow",
                1 => "orange1",
                2 => "red",
                3 => "orange1",
                _ => "grey"
            };
            builder.Append($"[{color}]{Markup.Escape(lines[i])}[/]");
            if (i < lines.Length - 1) {
                builder.AppendLine();
            }
        }
        return new Markup(builder.ToString());
    }
}
