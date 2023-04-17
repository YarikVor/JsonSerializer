namespace JsonSerializer.Abstractions;

public interface ITypedConversion<in TSource>
{
    object? ConvertToObject(TSource obj, Type type);
}