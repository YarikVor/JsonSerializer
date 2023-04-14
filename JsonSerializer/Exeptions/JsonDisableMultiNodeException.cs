namespace JsonSerializer;

public class JsonDisableMultiNodeException : Exception
{
    public JsonDisableMultiNodeException(string message = "Node must has an one node") : base(message)
    {
    }
}