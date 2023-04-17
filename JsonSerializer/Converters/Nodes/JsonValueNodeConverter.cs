using System.Text;
using JsonSerializer.Nodes.ValueNodes;

namespace JsonSerializer.Converters.Nodes;

internal class JsonValueNodeConverter : IJsonNodeConverter<ValueNode>
{
    public void Write(ValueNode node, StringBuilder sb, INodeToJsonConverter converter)
    {
        sb.Append(node.Value);
    }
}