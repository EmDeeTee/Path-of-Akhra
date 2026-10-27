using Game.Player.Skills;

namespace Game.Player;

// TODO: Extract HP logic to share with an enemy class
public class PlayerCharacter(int gold) {
    public int Gold { get; private set; } = gold;
    
    public int Health { get; private set; } = 100;

    public int MaxHealth { get; private set; } = 100;
    
    public object CurrentCombatTarget { get; set; }

    public ISkill[] Skills { get; private set; } = [
        new SkillStumblingStab()
    ];

    public void AddGold(int amount) {
        Gold += amount;
    }

    public int Heal(int amount) {
        Health += amount;
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }

        return Health - amount;
    }
}