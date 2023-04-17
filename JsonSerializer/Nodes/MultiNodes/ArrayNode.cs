using JsonSerializer.Attributes;

namespace JsonSerializer;

[JsonNodeConverter(typeof(JsonArrayNodeConverter))]
public class ArrayNode : MultiNode
{
}