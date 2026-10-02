namespace Game.Events;

public class StormEvent : IEvent {
    public EventResult Execute(GameSession gameSession) {
        string[] descriptions = [
            "A cold rain hounds you for hours. You press on, soaked and stubborn.",
            "The wind howls across the Path, bending the ancient trees like reeds.",
            "A storm breaks over the hills. You shelter beneath a fallen stone until it passes."
        ];

        return new EventResult(
            Description: descriptions[Random.Shared.Next(descriptions.Length)]
        );
    }
}
