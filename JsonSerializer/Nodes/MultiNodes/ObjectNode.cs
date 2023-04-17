using JsonSerializer.Attributes;

namespace JsonSerializer;

[JsonNodeConverter(typeof(JsonObjectNodeConverter))]
public class ObjectNode : MultiNode
{
}