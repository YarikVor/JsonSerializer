namespace JsonSerializer.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class JsonIgnoreAttribute : Attribute
{
    [Flags]
    public enum IgnorePropertyWhen
    {
        Never = 0,
        NullRef = 1,
        Default = 2,
        NullRefAndDefault = NullRef | Default,
        EmptyArray = 4,
        Empty = NullRef | Default | EmptyArray,
        Always = 8
    }

    public readonly IgnorePropertyWhen IgnoreWhen;

    public JsonIgnoreAttribute(IgnorePropertyWhen ignoreWhen = IgnorePropertyWhen.Never)
    {
        IgnoreWhen = ignoreWhen;
    }
}