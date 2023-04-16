using JsonSerializer.Nodes;

namespace JsonSerializer;

public class KeyNode : Node
{
    public string Name;

    private Node Node;

    public KeyNode()
    {
    }

    public KeyNode(string name)
    {
        Name = name;
    }

    public KeyNode(Node node)
    {
        Node = node;
    }

    public KeyNode(string name, Node node)
    {
        Name = name;
        Node = node;
    }

    public override NodeType NodeType => NodeType.OneNode;


    public override IEnumerator<Node> GetEnumerator()
    {
        yield return Node;
    }

    public override void Add(Node node)
    {
        if (Node != null)
            throw new JsonDisableMultiNodeException();

        Node = node;
    }
}