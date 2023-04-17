using System.Text;

namespace JsonSerializer;

internal abstract class JsonMultiNodeConverter<T> : IJsonNodeConverter<T> where T : MultiNode
{
    protected abstract char OpenBracket { get; }
    protected abstract char CloseBracket { get; }

    public void Write(T node, StringBuilder sb, INodeToJsonConverter converter)
    {
        sb.Append(OpenBracket);
        foreach (var child in node)
        {
            converter.SerializeEverything(child);
            sb.Append(JsonConstants.Comma);
        }

        if (sb[^1] == JsonConstants.Comma)
            sb.Length--;
        sb.Append(CloseBracket);
    }
}