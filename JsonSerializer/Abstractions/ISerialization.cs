namespace JsonSerializer;

public interface ISerialization<TSourse, TEntity>
{
    TEntity Serialize(TSourse sourse);
    TSourse Deserialize(TEntity result, Type type);
}