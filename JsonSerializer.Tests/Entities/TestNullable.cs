namespace JsonSerializer.Tests.Entities;

public class TestNullable<T> where T : struct
{
    public T? Value { get; set; }
}