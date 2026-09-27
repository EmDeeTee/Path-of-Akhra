namespace Game.Settlements;

public record Settlement(string Name, int Population) {
    public bool HasRested { get; set; } = false;
    
    public string PrintHeader() {
        return $"{Name} | Population: {Population}";
    }
}