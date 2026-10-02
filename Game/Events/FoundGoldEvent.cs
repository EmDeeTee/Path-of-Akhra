namespace Game.Events;

public class FoundGoldEvent : IEvent {
    public EventResult Execute(GameSession gameSession) {
        return new EventResult (
            Description: "You found some gold along the Path",
            FoundGold: Random.Shared.Next(100)
        );
    }
}