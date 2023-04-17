namespace JsonSerializer.Exeptions;

public class JsonInvalidNumberException : JsonInvalidValueConvertException
{
    public JsonInvalidNumberException(string? message) : base(message)
    {
    }
}