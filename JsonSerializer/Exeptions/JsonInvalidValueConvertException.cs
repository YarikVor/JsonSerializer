namespace JsonSerializer.Exeptions;

public class JsonInvalidValueConvertException : JsonException
{
    public JsonInvalidValueConvertException(string? message) : base(message)
    {
    }
}