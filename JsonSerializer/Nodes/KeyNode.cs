using JsonSerializer.Nodes;

namespace JsonSerializer;

public class KeyNode : Node
{
    public override NodeType NodeType => NodeType.OneNode;
    
    private Node Node = null;

    public string Name;
    public override IEnumerator<Node> GetEnumerator()
    {
        yield return Node;
    }

    public override void Push(Node node)
    {
        if (Node != null)
            throw new JsonDisableMultiNodeException();

        Node = node;
    }
}