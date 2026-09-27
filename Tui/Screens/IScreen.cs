namespace Tui.Screens;

public interface IScreen {
    /// <summary>
    /// Renders this screen and returns the next screen to display,
    /// or <c>null</c> to quit the game.
    /// </summary>
    IScreen? Show();
}