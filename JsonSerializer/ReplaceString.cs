using System.Text;

namespace JsonSerializer;

public static class ReplaceString
{
    private static readonly Dictionary<char, string> EscapeCharDictionary = new()
    {
        { '\0', @"\0" },
        { '\r', @"\r" },
        { '\n', @"\n" },
        { '\\', @"\\" },
        { '\t', @"\t" },
        { '\b', @"\b" },
        { '\f', @"\f" },
        { '\v', @"\v" },
        { '\a', @"\a" },
        { '\'', @"\'" }
    };

    public static string Replace(string? str)
    {
        return str == null
            ? string.Empty
            : ReplaceAsStringBuilder(str).ToString();
    }

    private static StringBuilder ReplaceAsStringBuilder(string str)
    {
        var sb = new StringBuilder(str.Length * 2);

        foreach (var c in str)
            if (EscapeCharDictionary.TryGetValue(c, out var replaceStr))
                sb.Append(replaceStr);
            else
                sb.Append(c);

        return sb;
    }

    public static string ReplaceChar(char c)
    {
        return EscapeCharDictionary.TryGetValue(c, out var replaceStr)
            ? replaceStr
            : c.ToString();
    }

    public static bool IsEscapeChar(char c)
    {
        return EscapeCharDictionary.ContainsKey(c);
    }
}