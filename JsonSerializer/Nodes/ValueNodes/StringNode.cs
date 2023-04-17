using JsonSerializer.Attributes;

namespace JsonSerializer;

[JsonNodeConverter(typeof(JsonStringNodeConverter))]
public class StringNode : ValueNode
{
    public StringNode()
    {
    }

    public StringNode(string value) : base(value)
    {
    }
}