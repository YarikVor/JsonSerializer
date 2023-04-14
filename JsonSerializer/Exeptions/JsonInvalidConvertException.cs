namespace JsonSerializer;

public class JsonInvalidConvertException : JsonException
{
    public JsonInvalidConvertException(string? message) : base(message)
    {
    }
}