namespace JsonSerializer.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class JsonTypeDataAttribute : Attribute
{
    public enum TypeData
    {
        Collection,
        Dictionary,
        Object,
        Primitive
    }

    public readonly TypeData DataType;

    public JsonTypeDataAttribute(TypeData typeData)
    {
        DataType = typeData;
    }
}