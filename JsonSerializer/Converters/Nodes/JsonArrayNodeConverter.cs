using JsonSerializer.Nodes.MultiNodes;

namespace JsonSerializer.Converters.Nodes;

internal class JsonArrayNodeConverter : JsonMultiNodeConverter<ArrayNode>
{
    protected override char OpenBracket => JsonConstants.OpenArray;
    protected override char CloseBracket => JsonConstants.CloseArray;
}