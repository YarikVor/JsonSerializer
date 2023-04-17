namespace JsonSerializer.Exeptions;

public class JsonKeyException : JsonException
{
    public JsonKeyException(string message = "A value was expected") : base(message)
    {
    }
}