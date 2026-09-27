using Game;
using Tui.Screens;
using Tui.Screens.MainMenu;

namespace Tui;

internal static class Program {
    private static void Main() {
        GameSession gameSession = new();
        
        IScreen? screen = new MainMenuScreen(gameSession);
        while (screen != null) {
            screen = screen.Show();
        }
    }
}