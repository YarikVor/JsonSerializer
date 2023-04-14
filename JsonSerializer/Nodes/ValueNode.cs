using JsonSerializer.Nodes;
using Newtonsoft.Json;

namespace JsonSerializer;

public abstract class ValueNode : Node
{
    public string Value { get; set; }

    public sealed override NodeType NodeType => NodeType.ValueType;

    public sealed override IEnumerator<Node> GetEnumerator()
    {
        yield break;
    }

    public sealed override void Push(Node node)
    {
        throw new NotImplementedException();
    }
}