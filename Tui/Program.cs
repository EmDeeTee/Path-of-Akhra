using Game;
using Tui.Screens;
using Tui.Screens.MainMenu;
using Tui.Screens.Town;

namespace Tui;

internal static class Program {
    private static void Main(string[] args) {
        GameSession gameSession = new();
        
        IScreen? screen = args.Contains("--skip-menu") ? new TownScreen(gameSession) : new MainMenuScreen(gameSession);
        while (screen != null) {
            screen = screen.Show();
        }
    }
}