using System.Collections;
using JsonSerializer.Attributes;
using JsonSerializer.Nodes;

namespace JsonSerializer;

public class NodeToObjectConverter :
    ITypedConversion<Node>
{
    public object? ConvertToObject(Node obj, Type type)
    {
        return ToObject(obj, type);
    }

    public object? ToObject(Node node, Type type)
    {
        if (type.IsPrimitive)
            return Convert.ChangeType(((ValueNode)node).Value, type);
        if (type == typeof(string))
            return ((StringNode)node).Value;
        if (type.IsArray) // T[]
        {
            var listType = JsonConstants.ListType; // List<>
            var elementType = type.GetElementType(); //  T[] -> T
            var genericType = listType.MakeGenericType(elementType); // List<> + T = List<T>
            var list = (IList)Activator.CreateInstance(genericType)!;
            foreach (var child in (ArrayNode)node)
                list.Add(ToObject(child, elementType));
            var destinationArray = Array.CreateInstance(elementType, list.Count);
            for (var i = 0; i < list.Count; i++)
                destinationArray.SetValue(list[i], i);

            return destinationArray;
        }

        if (type.IsGenericType
            && (type.GetInterfaces().Any(i => i == typeof(IDictionary<,>))
                || type.GetInterfaces().Any(i => i == typeof(IDictionary))))
        {
            var listType = typeof(Dictionary<,>);

            var keyElementType = type.GetGenericArguments().First();

            if (keyElementType != typeof(string))
                throw new JsonConverterNotSupportedException(
                    $"Dictionary key type {keyElementType} not supported (only string: Dictionary<string, T>)");

            var valueElementType = type.GetGenericArguments().Last();

            var genericType = listType.MakeGenericType(keyElementType, valueElementType);

            var list = (IDictionary)Activator.CreateInstance(genericType)!;

            foreach (var child in ((ObjectNode)node).Select(e => (KeyNode)e))
            {
                var valueNode = new StringNode(child.Name);
                var key = ToObject(valueNode, keyElementType);
                var value = ToObject(child.First(), valueElementType);

                list.Add(key, value);
            }

            return list;
        }

        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
        {
            if (node is NullNode nullNode) return null;
            return ToObject(node, underlyingType);
        }


        if (type.IsGenericType
            && (type.GetInterfaces().Any(i => i == typeof(IList<>))
                || type.GetInterfaces().Any(i => i == typeof(IList)))
           )
        {
            var listType = typeof(List<>);
            var elementType = type.GetGenericArguments().First();
            var genericType = listType.MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(genericType)!;
            foreach (var child in (ArrayNode)node)
                list.Add(ToObject(child, elementType));

            return list;
        }

        if (type.IsClass || type.IsValueType)
        {
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties();
            var objectNode = (ObjectNode)node;
            foreach (var property in properties.Where(p =>
                         p.CanWrite && p.GetIgnoreWhenJson() != JsonIgnoreAttribute.IgnorePropertyWhen.Always))
            {
                var keyNode = objectNode.FirstOrDefault(node => ((KeyNode)node).Name == property.Name);

                if (keyNode != null)
                    property.SetValue(obj, ToObject(keyNode.First(), property.PropertyType));
            }

            return obj;
        }

        throw new TypeAccessException();
    }
}