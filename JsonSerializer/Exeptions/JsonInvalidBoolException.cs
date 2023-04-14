namespace JsonSerializer;

public class JsonInvalidBoolException : JsonInvalidConvertException
{
    public JsonInvalidBoolException(string? message) : base(message)
    {
    }
}