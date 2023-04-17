using JsonSerializer.Attributes;
using JsonSerializer.Converters.Nodes;

namespace JsonSerializer.Nodes.ValueNodes;

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