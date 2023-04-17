namespace JsonSerializer.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class JsonNodeConverterAttribute : Attribute
{
    public readonly Type ConverterType;

    public JsonNodeConverterAttribute(Type converterType)
    {
        ConverterType = converterType;
    }
}