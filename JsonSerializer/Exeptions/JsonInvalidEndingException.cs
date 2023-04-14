namespace JsonSerializer;

public class JsonInvalidEndingException : JsonException
{
    public JsonInvalidEndingException(string? message) : base(message)
    {
    }
}