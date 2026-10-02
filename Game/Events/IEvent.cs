namespace Game.Events;

public interface IEvent {
    public EventResult Execute(GameSession gameSession);
}