using Game.Settlements;

namespace Game;

public sealed class GameSession {
    public Player Player { get; } = new(0);

    public Settlement CurrentSettlement { get; private set; } = new("Broken Beak", 576);
    public Settlement? TravelTargetSettlement  { get; private set; } = null;

    public void TravelTo(Settlement settlement) {
        CurrentSettlement = settlement;
    }
}