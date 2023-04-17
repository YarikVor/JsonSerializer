using System.Text;
using JsonSerializer.Nodes;

namespace JsonSerializer.Converters.Nodes;

internal interface IJsonNodeConverter<in TNode> : IJsonNodeConverter where TNode : Node
{
    void IJsonNodeConverter.Write(Node node, StringBuilder sb, INodeToJsonConverter converter)
    {
        Write((TNode)node, sb, converter);
    }

    void Write(TNode node, StringBuilder sb, INodeToJsonConverter converter);
}

public interface IJsonNodeConverter
{
    void Write(Node node, StringBuilder sb, INodeToJsonConverter converter);
}