namespace JsonSerializer.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class JsonPropertyNameAttribute : Attribute
{
    public readonly string Name;

    public JsonPropertyNameAttribute(string name)
    {
        Name = name;
    }
}