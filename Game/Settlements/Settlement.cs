namespace Game.Settlements;

public record Settlement(string Name, int Population) {
    public bool HasPlayerRested { get; set; }

    public string GetHeader() {
        return $"{Name} | Population: {Population}";
    }

    public static Settlement Next() {
        string[] names = [
            // ReSharper disable once StringLiteralTypo
            "Mudbreak",
            // ReSharper disable once StringLiteralTypo
            "Brambleton",
            // ReSharper disable once StringLiteralTypo
            "Holmfirth",
            // ReSharper disable once StringLiteralTypo
            "Oakhaven"
        ];

        return new Settlement(names[Random.Shared.Next(0, names.Length)], Random.Shared.Next(1000));
    }
}