using JsonSerializer.Nodes;

namespace JsonSerializer;

public abstract class ValueNode : Node
{
    public ValueNode()
    {
    }

    public ValueNode(string value) : this()
    {
        Value = value;
    }

    public string Value { get; set; }

    public sealed override NodeType NodeType => NodeType.ValueType;

    public sealed override IEnumerator<Node> GetEnumerator()
    {
        yield break;
    }

    public sealed override void Add(Node node)
    {
        throw new NotImplementedException();
    }
}