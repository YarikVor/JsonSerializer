using System.Collections;

namespace JsonSerializer;

public abstract class Node : IEnumerable<Node>
{
    private List<Node> body;

    public Range position;

    public IEnumerator<Node> GetEnumerator()
    {
        return body.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Node node)
    {
        body.Add(node);
    }
}