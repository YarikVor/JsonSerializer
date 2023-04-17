using Xunit.Abstractions;

namespace JsonSerializer.Tests;

public class JsonSerializationTests
{
    private readonly ITestOutputHelper _output;

    public JsonSerializationTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Serialization_ValidArg_ValidResult()
    {
        var serializer = new JsonSerializer();

        var expected = @"{""Title"":""str""}";

        var actual = serializer.Serialize(new { Title = "str" });

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Serialization_ValidArgs_ValidResults()
    {
        var serializer = new JsonSerializer();

        object nullRef = null;

        var actual = serializer.Serialize(new
        {
            Title = "str",
            Author = "str",
            YearPublished = 0,
            AuthorInfo = nullRef,
            IsAvailable = false,
            IsChecked = true,
            TestNullableType = (int?)null,
            TestNullableType2 = (int?)1
        });

        var expected
            = @"{""Title"":""str"",""Author"":""str"",""YearPublished"":0,""AuthorInfo"":null,""IsAvailable"":false,""IsChecked"":true,""TestNullableType"":null,""TestNullableType2"":1}";


        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(0.0)]
    [InlineData(-1)]
    [InlineData(-1.0)]
    [InlineData(1.0)]
    [InlineData(1.0e12)]
    [InlineData(1.0e-4)]
    [InlineData(-1.0e-4)]
    [InlineData(-1.0e+12)]
    public void Serialization_PrimitiveType_IsValid(object primitive)
    {
        if (!primitive.GetType().IsPrimitive)
            throw new Exception("It's not primitive type: " + primitive.GetType());

        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(new { value = primitive });

        var expected = string.Empty;

        if (primitive is bool b)
            expected = string.Format(@"{{""value"":{0}}}", b ? JsonConstants.True : JsonConstants.False);
        else
            expected = string.Format(@"{{""value"":{0}}}", primitive);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Serialization_Dictionary_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(new Dictionary<string, string>
        {
            { "key", "value" },
            { "key2", "value2" }
        });

        var expected = @"{""key"":""value"",""key2"":""value2""}";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Serialization_Array_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(new[]
        {
            "value",
            "value2"
        });

        var expected = @"[""value"",""value2""]";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Serialization_ArrayOfObjects_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(new[]
        {
            new { value = "value" },
            new { value = "value2" }
        });

        var expected = @"[{""value"":""value""},{""value"":""value2""}]";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Serialization_ArrayOfArrays_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(new[]
        {
            new[] { "value", "value2" },
            new[] { "value3", "value4" }
        });

        var expected = @"[[""value"",""value2""],[""value3"",""value4""]]";

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Serialization_List_IsValid()
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(new List<string>
        {
            "value",
            "value2"
        });

        var expected = @"[""value"",""value2""]";

        Assert.Equal(expected, actual);
    }
}