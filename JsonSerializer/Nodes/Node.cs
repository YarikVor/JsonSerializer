using System.Collections;

namespace JsonSerializer.Nodes;

public abstract class Node : IEnumerable<Node>
{
    public abstract IEnumerator<Node> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract void Add(Node node);
}