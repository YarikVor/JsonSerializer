using JsonSerializer.Nodes;

namespace JsonSerializer;

public abstract class MultiNode : Node
{
    public override NodeType NodeType => NodeType.MultiNode;

    private readonly List<Node> _nodes = new List<Node>();

    public override IEnumerator<Node> GetEnumerator() 
        => _nodes.GetEnumerator();

    public override void Push(Node node)
    {
        _nodes.Add(node);
    }
}