namespace JsonSerializer;

public class JsonInvalidNullException : JsonInvalidConvertException
{
    public JsonInvalidNullException(string? message = "") : base(message)
    {
    }
}