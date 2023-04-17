namespace JsonSerializer;

public class JsonInvalidNullException : JsonInvalidValueConvertException
{
    public JsonInvalidNullException(string? message = "") : base(message)
    {
    }
}