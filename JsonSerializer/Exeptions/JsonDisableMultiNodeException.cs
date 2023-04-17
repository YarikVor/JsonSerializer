namespace JsonSerializer.Exeptions;

public class JsonDisableMultiNodeException : JsonException
{
    public JsonDisableMultiNodeException(string message = "Node must has an one node") : base(message)
    {
    }
}