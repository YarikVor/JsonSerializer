using JsonSerializer.Attributes;

namespace JsonSerializer.Tests.Entities;

internal class BookCollection
{
    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.Default)]
    public int Id { get; set; }

    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.Empty)]
    public string[] Books { get; set; } = null!;
}