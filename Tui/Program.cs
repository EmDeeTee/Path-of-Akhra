using Game;
using Spectre.Console;
using Tui.Screens.MainMenu;

namespace Tui;

internal static class Program {
    private static void Main(string[] args) {
        GameSession gameSession = new();
        
        new MainMenuScreen(gameSession).Show();
    }
}