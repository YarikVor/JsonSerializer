namespace JsonSerializer;

public interface ITypedConversion<in TSourse>
{
    object? ConvertToObject(TSourse obj, Type type);
}