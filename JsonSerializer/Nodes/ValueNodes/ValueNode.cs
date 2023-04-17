using JsonSerializer.Attributes;
using JsonSerializer.Converters.Nodes;

namespace JsonSerializer.Nodes.ValueNodes;

[JsonNodeConverter(typeof(JsonValueNodeConverter))]
public abstract class ValueNode : Node
{
    public ValueNode()
    {
    }

    public ValueNode(string value) : this()
    {
        Value = value;
    }

    public string Value { get; set; } = null!;

    public sealed override IEnumerator<Node> GetEnumerator()
    {
        yield break;
    }

    public sealed override void Add(Node node)
    {
        throw new NotImplementedException();
    }
}