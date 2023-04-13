namespace JsonSerializer.Tests;

public class JsonSerializerTests
{

    public static IEnumerable<object[]> DataForJsonSerializer_Valid = new[]
    {
        new object[2]{new int[]{1, 2, 3, 4, 5}, "[1,2,3,4,5]"}
    };

    [Theory]
    [MemberData(nameof(DataForJsonSerializer_Valid))]
    public void JsonSerializer_Valid(object obj, string expected)
    {
        var serializer = new JsonSerializer();

        string actual = serializer.Serialize(obj);
        
        Assert.Equal(expected, actual);
    }
}