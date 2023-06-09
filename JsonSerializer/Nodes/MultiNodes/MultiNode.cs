namespace JsonSerializer.Nodes.MultiNodes;

public abstract class MultiNode : Node
{
    private readonly List<Node> _nodes = new();

    public override IEnumerator<Node> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    public override void Add(Node node)
    {
        _nodes.Add(node);
    }
}