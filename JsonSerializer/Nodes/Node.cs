using System.Collections;
using System.Runtime.Serialization;

namespace JsonSerializer;

public abstract class Node : IEnumerable<Node>
{
    public abstract bool IsMultiNodes { get; }

    private List<Node> body = new List<Node>();

    public Range range;

    public IEnumerator<Node> GetEnumerator()
    {
        return body.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Push(Node node)
    {
        if (!IsMultiNodes && body.Any())
            throw new JsonDisableMultiNodeException();

        body.Add(node);
    }
}