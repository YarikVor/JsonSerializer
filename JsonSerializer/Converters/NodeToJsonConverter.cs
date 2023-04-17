using System.Text;
using JsonSerializer.Attributes;
using JsonSerializer.Nodes;

namespace JsonSerializer;

public class NodeToJsonConverter :
    IConversion<Node, string>,
    INodeToJsonConverter
{
    private readonly Dictionary<Type, IJsonNodeConverter> _converters = new();

    private StringBuilder sb;

    string IConversion<Node, string>.ConvertTo(Node obj)
    {
        return ToJson(obj);
    }

    public void SerializeEverything(Node node)
    {
        if (TryGetJsonNodeConverter(node.GetType(), out var converter))
        {
            converter.Write(node, sb, this);
            return;
        }

        throw new JsonConverterNotSupportedException($"Node {node.GetType()} not supported");
    }

    public string ToJson(Node node)
    {
        sb = new StringBuilder(128);

        SerializeEverything(node);

        return sb.ToString();
    }

    private bool TryGetJsonNodeConverter(Type type, out IJsonNodeConverter converter)
    {
        if (_converters.TryGetValue(type, out converter)) return true;
        var attribute = Attribute.GetCustomAttribute(type, typeof(JsonNodeConverterAttribute));
        if (attribute is not JsonNodeConverterAttribute jsonNodeConverterAttribute) return false;
        var converterType = jsonNodeConverterAttribute.ConverterType;
        _converters[type]
            = converter
                = (IJsonNodeConverter)Activator.CreateInstance(converterType)!;

        return true;
    }
}