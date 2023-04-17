using JsonSerializer.Nodes;
using Newtonsoft.Json;

namespace JsonSerializer.Tests;

public partial class JsonSerializerTests
{
    public static IEnumerable<object[]> DataForJsonSerializer_Valid = new[]
    {
        new object[2] { new[] { 1, 2, 3, 4, 5 }, "[1,2,3,4,5]" },
        new object[2] { new { value = 23 }, @"{""value"":23}" },
        new object[2]
            { new { myObject = new { name = "Yarik", age = 23 } }, @"{""myObject"":{""name"":""Yarik"",""age"":23}}" },
        new object[2]
        {
            new { Number = 23, text = "Hello", isTrue = true }, @"{""Number"":23,""text"":""Hello"",""isTrue"":true}"
        },
        new object[2] { new Point(1, 2), @"{""X"":1,""Y"":2}" },
        new object[2]
        {
            new Person { FirstName = "Yarik", LastName = "Vorobyov", Age = 23, Location = new Point { X = 1, Y = 2 } },
            @"{""FirstName"":""Yarik"",""Age"":23,""Position"":{""X"":1,""Y"":2}}"
        },
        new object[2]
        {
            new Book
            {
                Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", YearPublished = 0,
                AuthorInfo = new Person
                    { FirstName = "Yarik", LastName = "Vorobyov", Age = 23, Location = new Point { X = 1, Y = 2 } }
            },
            @"{""Title"":""The Lord of the Rings"",""Author"":""J.R.R. Tolkien"",""AuthorInfo"":{""FirstName"":""Yarik"",""Age"":23,""Position"":{""X"":1,""Y"":2}}}"
        },
        new object[2]
        {
            new Book
            {
                Title = "The Lord of the Rings", Author = null, YearPublished = 0,
                AuthorInfo = new Person
                    { FirstName = "Yarik", LastName = "Vorobyov", Age = 23, Location = new Point { X = 1, Y = 2 } }
            },
            @"{""Title"":""The Lord of the Rings"",""AuthorInfo"":{""FirstName"":""Yarik"",""Age"":23,""Position"":{""X"":1,""Y"":2}}}"
        },
        new object[2]
        {
            new BookCollection
            {
                Books = new[] { "The Lord of the Rings", "The Hobbit", "The Silmarillion" },
                Id = 0
            },
            @"{""Books"":[""The Lord of the Rings"",""The Hobbit"",""The Silmarillion""]}"
        },
        new object[2]
        {
            new BookCollection
            {
                Books = new[] { "The Lord of the Rings", "The Hobbit", "The Silmarillion" },
                Id = 1
            },
            @"{""Id"":1,""Books"":[""The Lord of the Rings"",""The Hobbit"",""The Silmarillion""]}"
        },
        new object[2]
        {
            new BookCollection
            {
                Books = new string[] { },
                Id = 1
            },
            @"{""Id"":1}"
        },
        new object[2]
        {
            new BookCollection
            {
                Books = null,
                Id = 1
            },
            @"{""Id"":1}"
        },
        new object[2]
        {
            new BookCollection
            {
                Books = null,
                Id = 0
            },
            @"{}"
        }
    };


    [Theory]
    [MemberData(nameof(DataForJsonSerializer_Valid))]
    public void JsonSerializer_Valid(object obj, string expected)
    {
        var serialization = new JsonSerializer();
        var actual = serialization.Serialize(obj);
        Assert.NotNull(actual);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("{}", "[]")]
    [InlineData("[]", "[]")]
    [InlineData(@"{""key"":""value""}", "[[[]]]")]
    [InlineData(@"{""key"":""value"",""key2"":""value2""}", "[[[]],[[]]]")]
    [InlineData(@"[""1""]", "[[]]")]
    [InlineData(@"[""1"", ""2""]", "[[],[]]")]
    [InlineData(@"[""1"", ""2"", ""3""]", "[[],[],[]]")]
    [InlineData(@"[1]", "[[]]")]
    [InlineData(@"[1, 2]", "[[],[]]")]
    [InlineData(@"[1, 2, 3,]", "[[],[],[]]")]
    [InlineData(@"[true]", "[[]]")]
    [InlineData(@"[true, false]", "[[],[]]")]
    [InlineData(@"[null]", "[[]]")]
    [InlineData(@"[null, null]", "[[],[]]")]
    [InlineData(@"[{}]", "[[]]")]
    [InlineData(@"[{}, {}]", "[[],[]]")]
    [InlineData(@"[[]]", "[[]]")]
    [InlineData(@"[[], []]", "[[],[]]")]
    [InlineData(@"[1, ""2"", true, null, {}, []]", "[[],[],[],[],[],[]]")]
    [InlineData(@"[1.2]", "[[]]")]
    [InlineData(@"[1.2, 2.3]", "[[],[]]")]
    [InlineData(@"[1e2]", "[[]]")]
    [InlineData(@"[1e2, 1e-2]", "[[],[]]")]
    [InlineData(@"[-1.2]", "[[]]")]
    [InlineData(@"[-1.2, -2.3]", "[[],[]]")]
    [InlineData(@"[-1e2]", "[[]]")]
    [InlineData(@"[-1e2, -1e-2]", "[[],[]]")]
    [InlineData(@"[-1E2, -1E-2]", "[[],[]]")]
    [InlineData(@"[0]", "[[]]")]
    [InlineData(@"{""key"":1, ""key2"":2}", "[[[]],[[]]]")]
    [InlineData(@"{""key"":1, ""key2"":[], ""key3"":{}}", "[[[]],[[]],[[]]]")]
    public void JsonTree_EqualPseudoArray(string json, string pseudoArray)
    {
        var analyzator = new JsonToNodeConverter();

        var nodeActual = analyzator.Read(json);

        var actual = JsonConvert.SerializeObject(nodeActual);

        Assert.Equal(pseudoArray, actual);
    }


    [Theory]
    [InlineData(@"{1}")]
    [InlineData(@"{")]
    [InlineData(@"}")]
    [InlineData(@"{""key"":")]
    [InlineData(@"{""key"":""}")]
    [InlineData(@"{""key"":wer")]
    [InlineData(@"[")]
    [InlineData(@"]")]
    [InlineData(@"[1")]
    [InlineData(@"[1,")]
    [InlineData(@"[1,}")]
    [InlineData(@"{1")]
    [InlineData(@"{""key"":,")]
    [InlineData(@"{""key"":,")]
    [InlineData(@"{""key"",")]
    public void JsonTree_Throw(string json)
    {
        var analyzator = new JsonToNodeConverter();

        Node nodes;

        try
        {
            nodes = analyzator.Read(json);
        }
        catch (JsonException e)
        {
            _output.WriteLine($"JsonException: {e.GetType().Name}");
            return;
        }
        catch (Exception e)
        {
            Assert.Fail(e.ToString());
        }

        Assert.Fail("No exception");
    }
}