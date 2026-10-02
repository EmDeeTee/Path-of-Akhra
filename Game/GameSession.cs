using Game.Events;
using Game.Settlements;

namespace Game;

public sealed class GameSession {
    public Player Player { get; } = new(0);

    public Settlement CurrentSettlement { get; private set; } = new("Lonely Road", 1);

    public Settlement? TravelTargetSettlement { get; private set; }
    
    public IEvent[]? CurrentEventSequence { get; private set;  }

    public void TravelTo(Settlement settlement) {
        TravelTargetSettlement = settlement;
        CurrentEventSequence = GenerateNewEventSequence();
    }
    
    public void CompleteTravel() {
        if (TravelTargetSettlement is null || CurrentEventSequence is null) {
            throw new InvalidOperationException("The pilgrim is not currently travelling.");
        }

        CurrentSettlement = TravelTargetSettlement;
        TravelTargetSettlement = null;
        CurrentEventSequence = null;
    }
    
    public void ApplyEventResult(EventResult result) {
        if (result.FoundGold > 0) {
            Player.AddGold(result.FoundGold);
        }

        if (result.HealedHealth > 0) {
            Player.Heal(result.HealedHealth);
        }
    }

    private static IEvent[] GenerateNewEventSequence() {
        IEvent[] possibleEvents = [
            new FoundGoldEvent(),
            new RestingTravelerEvent(),
            new StormEvent()
        ];

        int eventCount = Random.Shared.Next(1, 4);
        return Enumerable.Range(0, eventCount)
            .Select(_ => possibleEvents[Random.Shared.Next(possibleEvents.Length)])
            .ToArray();
    }
}