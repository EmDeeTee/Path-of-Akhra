namespace Game;

public class Player(int gold) {
    public int Gold { get; private set; } = gold;
    
    public int Health { get; private set; } = 100;

    public int MaxHealth { get; private set; } = 100;

    public int Heal(int amount) {
        Health += amount;
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }

        return Health - amount;
    }
}