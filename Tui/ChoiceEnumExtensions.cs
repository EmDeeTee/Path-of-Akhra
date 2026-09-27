using System.Text.RegularExpressions;

namespace Tui;

public static class ChoiceEnumExtensions {
    public static string ToDisplayName<TEnum>(this TEnum value) where TEnum : struct, Enum {
        return Regex.Replace(
            value.ToString(),
            "(?<!^)([A-Z])",
            " $1"
        );
    }
}