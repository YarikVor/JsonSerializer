using JsonSerializer.Attributes;
using JsonSerializer.Converters.Nodes;
using JsonSerializer.Exeptions;

namespace JsonSerializer.Nodes;

[JsonNodeConverter(typeof(JsonKeyNodeConverter))]
public class KeyNode : Node
{
    public string Name = null!;

    private Node? _node;

    public KeyNode()
    {
    }

    public KeyNode(string name)
    {
        Name = name;
    }

    public KeyNode(Node node)
    {
        _node = node;
    }

    public KeyNode(string name, Node node)
    {
        Name = name;
        _node = node;
    }


    public override IEnumerator<Node> GetEnumerator()
    {
        yield return _node!;
    }

    public override void Add(Node node)
    {
        if (_node != null)
            throw new JsonDisableMultiNodeException();

        _node = node;
    }
}