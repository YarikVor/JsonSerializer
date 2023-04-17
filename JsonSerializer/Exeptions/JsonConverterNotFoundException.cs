namespace JsonSerializer;

public class JsonConverterNotSupportedException : JsonException
{
    public JsonConverterNotSupportedException(string? message) : base(message)
    {
    }
}