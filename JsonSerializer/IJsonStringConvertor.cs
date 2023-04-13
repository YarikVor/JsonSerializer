namespace JsonSerializer;

public interface IJsonStringConvertor<T>
{
    string Write(T value);
    T Read(string str);
}