using JsonSerializer.Attributes;

namespace JsonSerializer.Tests;

internal class Person
{
    public string FirstName { get; set; }

    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.Always)]
    public string LastName { get; set; }

    public int Age { get; set; }

    [JsonPropertyName("Position")]
    public Point Location { get; set; }
}