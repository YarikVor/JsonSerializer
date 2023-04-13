namespace JsonSerializer;

public class JsonException : Exception
{
    public JsonException(string? message) : base(message)
    {
    }

    public static void Throw(string? message)
    {
        throw new JsonException(message);
    }
}