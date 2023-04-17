using System.Collections;
using System.Reflection;
using JsonSerializer.Attributes;

namespace JsonSerializer.Extensions;

public static class JsonExtension
{
    public static string GetPropertyNameJson(this PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attribute?.Name ?? propertyInfo.Name;
    }

    public static JsonIgnoreAttribute.IgnorePropertyWhen GetIgnoreWhenJson(this PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();
        return attribute?.IgnoreWhen ?? JsonIgnoreAttribute.IgnorePropertyWhen.Never;
    }

    public static bool IsIgnore(this PropertyInfo property, object? value)
    {
        var ignore = property.GetIgnoreWhenJson();

        if (ignore == JsonIgnoreAttribute.IgnorePropertyWhen.Never)
            return false;

        if (ignore == JsonIgnoreAttribute.IgnorePropertyWhen.Always)
            return true;

        if ((ignore
             & (JsonIgnoreAttribute.IgnorePropertyWhen.NullRef | JsonIgnoreAttribute.IgnorePropertyWhen.Default)) != 0
            && value == null)
            return true;

        if ((ignore & JsonIgnoreAttribute.IgnorePropertyWhen.Default) != 0
            && property.PropertyType.IsValueType
            && value.Equals(Activator.CreateInstance(property.PropertyType)))
            return true;

        if ((ignore & JsonIgnoreAttribute.IgnorePropertyWhen.EmptyArray) != 0
            && value is IEnumerable enumerable
            && !enumerable.GetEnumerator().MoveNext())
            return true;

        return false;
    }
}