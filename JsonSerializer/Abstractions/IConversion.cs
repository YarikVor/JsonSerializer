namespace JsonSerializer.Abstractions;

public interface IConversion<in TSource, out TEntity>
{
    TEntity ConvertTo(TSource source);
}