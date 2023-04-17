using JsonSerializer.Abstractions;
using JsonSerializer.Converters;
using JsonSerializer.Nodes;

namespace JsonSerializer;

public class JsonSerializer :
    ISerialization<object, string>,
    IConversion<object, string>,
    ITypedConversion<string>
{
    private readonly IConversion<string, Node> _jsonToNodeConverter = new JsonToNodeConverter();
    private readonly IConversion<Node, string> _nodeToJsonConverter = new NodeToJsonConverter();
    private readonly ITypedConversion<Node> _nodeToObjectConverter = new NodeToObjectConverter();
    private readonly IConversion<object, Node> _objectToNodeConverter = new ObjectToNodeConverter();

    string IConversion<object, string>.ConvertTo(object obj)
    {
        return Serialize(obj);
    }

    public string Serialize(object obj)
    {
        var nodes = _objectToNodeConverter.ConvertTo(obj);
        return _nodeToJsonConverter.ConvertTo(nodes);
    }

    public object Deserialize(string obj, Type type)
    {
        var nodes = _jsonToNodeConverter.ConvertTo(obj);
        return _nodeToObjectConverter.ConvertToObject(nodes, type)!;
    }

    object ITypedConversion<string>.ConvertToObject(string obj, Type type)
    {
        return Deserialize(obj, type);
    }
}