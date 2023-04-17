namespace JsonSerializer.Exeptions;

public class JsonException : Exception
{
    public JsonException(string? message) : base(message)
    {
    }
}