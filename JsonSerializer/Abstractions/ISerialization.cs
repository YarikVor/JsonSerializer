namespace JsonSerializer.Abstractions;

public interface ISerialization<TSource, TEntity>
{
    TEntity Serialize(TSource source);
    TSource Deserialize(TEntity result, Type type);
}