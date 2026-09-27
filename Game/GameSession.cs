using Game.Settlements;

namespace Game;

public sealed class GameSession {
    public Player Player { get; } = new(0);

    public Settlement CurrentSettlement { get; private set; } = new("Test2", 1000);

    public void TravelTo(Settlement settlement) {
        CurrentSettlement = settlement;
    }
}