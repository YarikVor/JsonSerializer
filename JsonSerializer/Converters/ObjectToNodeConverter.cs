using System.Collections;
using JsonSerializer.Abstractions;
using JsonSerializer.Extensions;
using JsonSerializer.Nodes;
using JsonSerializer.Nodes.MultiNodes;
using JsonSerializer.Nodes.ValueNodes;

namespace JsonSerializer.Converters;

public class ObjectToNodeConverter :
    IConversion<object, Node>
{
    public Node ConvertTo(object obj)
    {
        return ToNode(obj);
    }

    public Node ToNode(object obj)
    {
        return Serialize(obj, obj.GetType());
    }

    private MultiNode Serialize(object obj, Type type)
    {
        MultiNode rootNode;

        if (obj is IDictionary dictionary)
        {
            rootNode = new ObjectNode();
            ReadElementAsDictionary(dictionary, rootNode);
        }
        else if (obj is IEnumerable)
        {
            rootNode = new ArrayNode();
            ReadElementAsArray(obj, rootNode);
        }
        else
        {
            rootNode = new ObjectNode();
            ReadElementAsObject(obj, type, rootNode);
        }


        return rootNode;
    }

    private void ReadElementAsObject(object parentObj, Type type, Node parent)
    {
        var properties = type.GetProperties();

        foreach (var property in properties.Where(p => p.CanRead))
        {
            var value = property.GetValue(parentObj);

            if (property.IsIgnore(value))
                continue;

            var keyNode = new KeyNode(
                property.GetPropertyNameJson(),
                GetNode(value)
            );

            parent.Add(keyNode);
        }
    }

    private void ReadElementAsDictionary(IDictionary dictionary, Node parent)
    {
        var dictionaryEnumerable = dictionary.GetEnumerator();

        while (dictionaryEnumerable.MoveNext())
        {
            var value = dictionaryEnumerable.Value;
            var key = dictionaryEnumerable.Key?.ToString() ?? JsonConstants.Null;
            var keyNode = new KeyNode(key, GetNode(value));
            parent.Add(keyNode);
        }
    }

    private Node GetNode(object? value)
    {
        if (value == null || value.Equals(null)) return ReadAsNull();

        var valueType = value.GetType();

        if (valueType.IsPrimitive) return ReadAsPrimitive(value);
        if (value is string) return ReadString(value);

        var underlyingType = Nullable.GetUnderlyingType(valueType);
        if (underlyingType != null)
        {
            var underlyingPropertyInfo = valueType.GetProperty(nameof(Nullable<bool>.Value));
            var underlyingValue = underlyingPropertyInfo!.GetValue(value);
            return GetNode(underlyingValue);
        }

        if (value is IDictionary dictionary)
        {
            var node = new ObjectNode();
            ReadElementAsDictionary(dictionary, node);
            return node;
        }
        
        if (value is IEnumerable)
        {
            var node = new ArrayNode();
            ReadElementAsArray(value, node);
            return node;
        }

        if (valueType.IsClass || valueType.IsValueType)
        {
            var node = new ObjectNode();
            ReadElementAsObject(value, valueType, node);
            return node;
        }

        throw new TypeAccessException();
    }


    private void ReadElementAsArray(object value, Node parent)
    {
        if (value is not IEnumerable enumerable)
            throw new TypeAccessException();

        foreach (var obj in enumerable)
            parent.Add(GetNode(obj));
    }

    private NumberNode ReadNumber(object obj)
    {
        return new NumberNode(obj.ToString()!);
    }

    private BoolNode ReadBool(object obj)
    {
        return new BoolNode((bool)obj
            ? JsonConstants.True
            : JsonConstants.False);
    }

    private StringNode ReadString(object obj)
    {
        return new StringNode(obj.ToString()!);
    }

    private NullNode ReadAsNull()
    {
        return new NullNode();
    }

    private ValueNode ReadAsPrimitive(object obj)
    {
        if (obj is bool)
            return ReadBool(obj);

        return ReadNumber(obj);
    }
}