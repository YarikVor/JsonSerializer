namespace JsonSerializer;

public class JsonInvalidNumberException : JsonInvalidValueConvertException
{
    public JsonInvalidNumberException(string? message) : base(message)
    {
    }
}