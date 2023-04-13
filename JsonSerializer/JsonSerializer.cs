namespace JsonSerializer;

public class JsonSerializer 
{
    public string Serialize(object obj)
    {
        return "";
    }

    public TEntity Deserialize<TEntity>() where TEntity: class
    {
        return default;
    }
}