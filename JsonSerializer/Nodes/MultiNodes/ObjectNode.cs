using JsonSerializer.Attributes;
using JsonSerializer.Converters.Nodes;

namespace JsonSerializer.Nodes.MultiNodes;

[JsonNodeConverter(typeof(JsonObjectNodeConverter))]
public class ObjectNode : MultiNode
{
}