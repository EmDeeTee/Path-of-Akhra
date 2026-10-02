namespace Game.Events;

public class RestingTravelerEvent : IEvent {
    public EventResult Execute(GameSession gameSession) {
        int healedHealth = Math.Min(
            Random.Shared.Next(5, 21),
            gameSession.Player.MaxHealth - gameSession.Player.Health
        );

        string description = healedHealth > 0
            ? "You share a fire with a wandering healer, who tends to your wounds."
            : "You share a fire with a wandering healer, but your wounds are already clean.";

        return new EventResult(
            Description: description,
            HealedHealth: healedHealth
        );
    }
}
