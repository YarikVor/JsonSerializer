using Xunit.Abstractions;

namespace JsonSerializer.Tests;

public partial class JsonSerializerTests
{
    private readonly ITestOutputHelper _output;

    public JsonSerializerTests(ITestOutputHelper output)
    {
        _output = output;
    }
}