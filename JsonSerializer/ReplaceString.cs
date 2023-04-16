using System.Text;

namespace JsonSerializer;

public static class ReplaceString
{
    static readonly Dictionary<char, string> ReplaceDictionary = new()
    {
        { '\0', @"\0" },
        { '\r', @"\r" },
        { '\n', @"\n" },
        { '\\', @"\\" },
        { '\t', @"\t" }
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
            if (ReplaceDictionary.TryGetValue(c, out var replaceStr))
                sb.Append(replaceStr);
            else
                sb.Append(c);

        return sb;
    }
}