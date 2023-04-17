namespace JsonSerializer;

public static class JsonConstants
{
    public const string Null = "null";
    public const string True = "true";
    public const string False = "false";
    public const char OpenObject = '{';
    public const char CloseObject = '}';
    public const char OpenArray = '[';
    public const char CloseArray = ']';
    public const char Comma = ',';
    public const char Colon = ':';
    public const char Quote = '\"';
    public const char Escape = '\\';
    public static readonly Type ListType = typeof(List<>);
}