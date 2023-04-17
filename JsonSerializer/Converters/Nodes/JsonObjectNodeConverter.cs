namespace JsonSerializer;

internal class JsonObjectNodeConverter : JsonMultiNodeConverter<ObjectNode>
{
    protected override char OpenBracket => JsonConstants.OpenObject;
    protected override char CloseBracket => JsonConstants.CloseObject;
}