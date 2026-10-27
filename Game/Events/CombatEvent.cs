namespace Game.Events;

public class CombatEvent : IEvent {
    public EventResult Execute(GameSession gameSession) {
        return new EventResult(
            Description: "Combat",
            Enemy: "hello"
        );
    }
}