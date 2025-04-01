using System.Text.RegularExpressions;

namespace Api.Validations;

public class RegexValidator
{
    private static readonly Dictionary<string, string> _regexPatterns = new Dictionary<string, string>
    {
        { "Username", @"^[^\s]{4,16}$" },
        { "Email", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" },
        { "Phone", @"^\d{10}$" },
        { "Number", @"^\d+$" },
        { "Password", @"^.{6,}$" }
    };

    public static bool Validate(string key, string value)
    {
        if (!_regexPatterns.ContainsKey(key)) return false;
        return Regex.IsMatch(value, _regexPatterns[key]);
    }
}