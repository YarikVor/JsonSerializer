using JsonSerializer.Abstractions;

namespace JsonSerializer.Extensions;

public static class SerializationExtension
{
    public static TEntity Deserialize<TEntity, TSource>(this ISerialization<object, TSource> serialization, TSource str)
    {
        return (TEntity)serialization.Deserialize(str, typeof(TEntity));
    }

    public static TEntity ConvertToObject<TEntity, TSource>(this ITypedConversion<TSource> serialization, TSource str)
    {
        return (TEntity)serialization.ConvertToObject(str, typeof(TEntity))!;
    }
}