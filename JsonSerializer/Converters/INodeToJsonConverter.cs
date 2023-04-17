using JsonSerializer.Nodes;

namespace JsonSerializer;

public interface INodeToJsonConverter
{
    void SerializeEverything(Node node);
}