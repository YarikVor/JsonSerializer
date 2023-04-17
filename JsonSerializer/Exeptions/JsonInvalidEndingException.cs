namespace JsonSerializer;

public class JsonEndException : JsonException
{
    public JsonEndException(string? message) : base(message)
    {
    }
}