using System.Text;

namespace JsonSerializer;

internal class JsonValueNodeConverter : IJsonNodeConverter<ValueNode>
{
    public void Write(ValueNode node, StringBuilder sb, INodeToJsonConverter converter)
    {
        sb.Append(node.Value);
    }
}