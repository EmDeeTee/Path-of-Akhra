namespace Game.Events;

public record EventResult(string Description, int FoundGold = 0, int HealedHealth = 0);