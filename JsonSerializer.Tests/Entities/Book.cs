using JsonSerializer.Attributes;

namespace JsonSerializer.Tests;

internal class Book
{
    public string Title { get; set; }

    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.NullRef)]
    public string Author { get; set; }

    [JsonIgnore(JsonIgnoreAttribute.IgnorePropertyWhen.Default)]
    public int YearPublished { get; set; }

    public Person AuthorInfo { get; set; }
}