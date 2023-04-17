using JsonSerializer.Nodes;

namespace JsonSerializer.Converters;

public interface INodeToJsonConverter
{
    void SerializeEverything(Node node);
}