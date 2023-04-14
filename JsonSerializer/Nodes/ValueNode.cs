using Newtonsoft.Json;

namespace JsonSerializer;

public abstract class ValueNode : Node
{
    public override bool IsMultiNodes => false;

    [JsonProperty]
    public string Value { get; set; }
}