namespace JsonSerializer;

public interface IConversion<in TSourse, out TEntity>
{
    TEntity ConvertTo(TSourse sourse);
}