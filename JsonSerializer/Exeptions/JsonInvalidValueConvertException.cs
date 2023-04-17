namespace JsonSerializer;

public class JsonInvalidValueConvertException : JsonException
{
    public JsonInvalidValueConvertException(string? message) : base(message)
    {
    }
}