namespace Game.Player.Skills;

public interface ISkill {
    public string GetName(GameSession gameSession);
    public int GetDamage(GameSession gameSession);
}