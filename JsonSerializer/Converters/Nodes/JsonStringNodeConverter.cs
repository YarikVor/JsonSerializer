using System.Text;
using JsonSerializer.Nodes.ValueNodes;

namespace JsonSerializer.Converters.Nodes;

internal class JsonStringNodeConverter : IJsonNodeConverter<StringNode>
{
    public void Write(StringNode node, StringBuilder sb, INodeToJsonConverter converter)
    {
        var replaceString = ReplaceString.Replace(node.Value);
        sb.Append(JsonConstants.Quote);
        sb.Append(replaceString);
        sb.Append(JsonConstants.Quote);
    }
}