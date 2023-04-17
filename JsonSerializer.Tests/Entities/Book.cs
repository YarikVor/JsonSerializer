using JsonSerializer.Attributes;

namespace JsonSerializer.Tests.Entities;

internal class Book
{
    public string Title { get; set; } = null!;

    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.NullRef)]
    public string Author { get; set; }= null!;

    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.Default)]
    public int YearPublished { get; set; }

    public Person AuthorInfo { get; set; }= null!;
}