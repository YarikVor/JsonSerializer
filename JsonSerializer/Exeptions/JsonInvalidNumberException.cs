namespace JsonSerializer;

public class JsonInvalidNumberException : JsonInvalidConvertException
{
    public JsonInvalidNumberException(string? message) : base(message)
    {
        
    }
}