using System.Text.RegularExpressions;

namespace Tui.Screens.MainMenu;

public enum MainMenuChoice
{
    WalkThePath,
    HallOfFame,
    Quit
}

public static class MainMenuChoiceExtensions
{
    public static string ToDisplayName(this MainMenuChoice value)
    {
        return Regex.Replace(
            value.ToString(),
            "(?<!^)([A-Z])",
            " $1"
        );
    }
}