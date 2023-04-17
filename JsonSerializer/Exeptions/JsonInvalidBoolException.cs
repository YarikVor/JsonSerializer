namespace JsonSerializer.Exeptions;

public class JsonInvalidBoolException : JsonInvalidValueConvertException
{
    public JsonInvalidBoolException(string? message) : base(message)
    {
    }
}