namespace Game.Player.Skills;

public class SkillStumblingStab : ISkill {
    public string GetName(GameSession gameSession) {
        return gameSession.Player.Gold == 666 ? "Devilish stab" : "Stumbling stab";
    }
    
    public int GetDamage(GameSession gameSession) {
        return 2;
    }
}