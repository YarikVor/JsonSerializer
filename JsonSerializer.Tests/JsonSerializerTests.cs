namespace JsonSerializer.Tests;

public class JsonSerializerTests
{
    public static IEnumerable<object[]> DataForJsonSerializer_Valid = new[]
    {
        new object[2] { new[] { 1, 2, 3, 4, 5 }, "[1,2,3,4,5]" },
        new object[2] { new { value = 23 }, "{'value':23}" }
    };

    [Theory]
    [MemberData(nameof(DataForJsonSerializer_Valid))]
    public void JsonSerializer_Valid(object obj, string expected)
    {
        var serializer = new JsonSerializer();

        var actual = serializer.Serialize(obj);

        Assert.Equal(expected, actual);
    }
}