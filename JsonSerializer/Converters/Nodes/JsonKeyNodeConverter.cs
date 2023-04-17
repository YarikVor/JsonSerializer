using System.Text;
using JsonSerializer.Nodes;

namespace JsonSerializer.Converters.Nodes;

internal class JsonKeyNodeConverter : IJsonNodeConverter<KeyNode>
{
    public void Write(KeyNode node, StringBuilder sb, INodeToJsonConverter converter)
    {
        sb.Append(JsonConstants.Quote);
        sb.Append(node.Name);
        sb.Append(JsonConstants.Quote);
        sb.Append(JsonConstants.Colon);
        converter.SerializeEverything(node.First());
    }
}