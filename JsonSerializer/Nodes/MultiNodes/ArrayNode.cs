using JsonSerializer.Attributes;
using JsonSerializer.Converters.Nodes;

namespace JsonSerializer.Nodes.MultiNodes;

[JsonNodeConverter(typeof(JsonArrayNodeConverter))]
public class ArrayNode : MultiNode
{
}