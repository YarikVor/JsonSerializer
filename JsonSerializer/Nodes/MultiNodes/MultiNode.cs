using JsonSerializer.Nodes;

namespace JsonSerializer;

public abstract class MultiNode : Node
{
    private readonly List<Node> _nodes = new();
    public override NodeType NodeType => NodeType.MultiNode;

    public override IEnumerator<Node> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    public override void Add(Node node)
    {
        _nodes.Add(node);
    }
}