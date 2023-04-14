namespace JsonSerializer.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class JsonIgnoreAttribute : Attribute
{
    [Flags]
    public enum IgnorePropertyWhen
    {
        None = 0,
        NullRef = 1,
        Default = 2,
        NullRefAndDefault = NullRef | Default,
        EmptyArray = 4,
        Empty = NullRef | Default | EmptyArray,
        Always = 8
    }
    
    public readonly IgnoreWhen IgnoreWhen;
    
    public JsonIgnoreAttribute(IgnorePropertyWhen ignoreWhen = IgnorePropertyWhen.Empty)
    {
        IgnoreWhen = ignoreWhen;
    }
}