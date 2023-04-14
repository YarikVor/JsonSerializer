namespace JsonSerializer;

public class JsonEndElementExeption : JsonException
{
    public JsonEndElementExeption(string? message = "Element doesn't have end symbol") : base(message)
    {
    }
}