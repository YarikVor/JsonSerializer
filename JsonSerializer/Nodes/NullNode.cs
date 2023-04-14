namespace JsonSerializer;

public class NullNode : ValueNode
{
    public override bool IsMultiNodes => false;

    public NullNode()
    {
        this.Value = "null";
    }
}