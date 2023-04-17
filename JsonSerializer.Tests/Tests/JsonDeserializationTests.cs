using JsonSerializer.Extensions;
using JsonSerializer.Tests.Entities;

namespace JsonSerializer.Tests.Tests;

public class JsonDeserializationTests
{
    [Fact]
    public void Deserialization_StringArray_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Deserialize<string[], string>(@"[""value"",""value2""]");

        var expected = new[]
        {
            "value",
            "value2"
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Deserialization_IntArray_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Deserialize<int[], string>("[1,2,3,4,5]");

        var expected = new[] { 1, 2, 3, 4, 5 };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Deserialization_Nullable_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Deserialize<TestNullable<int>, string>(@"{""Value"":1}");

        var expected = 1;

        Assert.Equal(expected, actual.Value);
    }

    [Fact]
    public void Deserialization_Nullable_Null_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Deserialize<TestNullable<int>, string>(@"{""Value"":null}");

        Assert.Null(actual.Value);
    }

    [Fact]
    public void Deserialization_Dictionary_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual
            = serializer.Deserialize<Dictionary<string, string>, string>(@"{""key"":""value"",""key2"":""value2""}");

        var expected = new Dictionary<string, string>
        {
            { "key", "value" },
            { "key2", "value2" }
        };

        Assert.Equal(expected, actual);
    }
}