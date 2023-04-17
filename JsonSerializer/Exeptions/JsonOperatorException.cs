namespace JsonSerializer.Exeptions;

public class JsonOperatorException : JsonException
{
    public JsonOperatorException(string? message) : base(message)
    {
    }
}