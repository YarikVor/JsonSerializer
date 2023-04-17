using System.Text;
using JsonSerializer.Nodes;

namespace JsonSerializer;

public class JsonToNodeConverter :
    IConversion<string, Node>,
    IConversion<Stream, Node>
{
    public Node ConvertTo(Stream sourse)
    {
        return Read(sourse);
    }

    public Node ConvertTo(string sourse)
    {
        return Read(sourse);
    }


    private Node ReadFromStream()
    {
        var isOpenKeyNode = false;

        while (MoveNext())
        {
            var symbol = CurrentChar;

            switch (symbol) // "key":
            {
                case ' ' or '\t' or '\r' or '\n':
                    continue;
                case '"' when FirstElement is ObjectNode && !isOpenKeyNode:
                    Push(ReadKeyNode());
                    continue;
                case ',' when !isOpenKeyNode:
                    break;
                case ':' when FirstElement is KeyNode && !isOpenKeyNode:
                    isOpenKeyNode = true;
                    continue;
                case '{':
                    Push(new ObjectNode());
                    break;
                case '[':
                    Push(new ArrayNode());
                    break;
                case '"':
                {
                    FirstElement.Add(
                        ReadStringNode()
                    );
                    break;
                }
                case '}' when FirstElement is ObjectNode:
                {
                    PopToFirstChild();
                    break;
                }
                case ']' when FirstElement is ArrayNode:
                {
                    PopToFirstChild();
                    break;
                }
                case not '"' when FirstElement is ObjectNode && !isOpenKeyNode:
                    throw new JsonKeyException();
                case '-' or (>= '0' and <= '9'):
                {
                    PositionDecrement();
                    var node = ReadAsNumber();
                    PositionDecrement();
                    FirstElement.Add(node);
                    break;
                }
                case 't' or 'f':
                {
                    PositionDecrement();
                    var node = ReadAsBool();
                    FirstElement.Add(node);
                    break;
                }
                case 'n':
                {
                    PositionDecrement();
                    var node = ReadAsNull();
                    FirstElement.Add(node);
                    break;
                }

                default:
                    throw new JsonOperatorException($"Error convert operator '{symbol}'");
            }

            isOpenKeyNode = false;
            if (FirstElement is KeyNode)
                PopToFirstChild();
        }

        if (_stack.Count == 1)
            return Root.First();

        throw new JsonEndException("");
    }

    #region private fields

    private readonly Stack<Node> _stack = new(16);
    private Node Root = new KeyNode();
    private BinaryEnumerable _binaryEnumerable;

    #endregion


    #region private methods for reading

    private bool MoveNext()
    {
        return _binaryEnumerable.MoveNext();
    }

    private Node FirstElement
        => _stack.Peek();

    private char CurrentChar
        => _binaryEnumerable.Current;

    private void PositionDecrement()
    {
        _binaryEnumerable.Back();
    }

    private void Push(Node node)
    {
        _stack.Push(node);
    }

    private void PopToFirstChild()
    {
        var node = _stack.Pop();
        FirstElement.Add(node);
    }

    #endregion


    #region public methods

    public Node Read(string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        var bytes = Encoding.UTF8.GetBytes(str);
        var stream = new MemoryStream(bytes);
        return Read(stream);
    }

    private Node Read(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        _binaryEnumerable = new BinaryEnumerable(stream);

        Push(new ObjectNode());
        Push(Root = new KeyNode());
        return ReadFromStream();
    }

    #endregion


    #region read nodes

    private KeyNode ReadKeyNode()
    {
        return new KeyNode(ReadString());
    }

    private StringNode ReadStringNode()
    {
        return new StringNode(ReadString());
    }

    private string ReadString()
    {
        var sb = new StringBuilder();
        var isBackSlash = false;

        while (MoveNext())
        {
            var symbol = CurrentChar;

            if (isBackSlash)
            {
                isBackSlash = false;

                var convert = symbol switch
                {
                    '"' or '\\' => symbol,
                    'n' => '\n',
                    'r' => '\r',
                    '0' => '\0',
                    't' => '\t',
                    _ => char.MaxValue
                };

                if (convert != char.MaxValue)
                    sb.Append(convert);
            }
            else if (symbol == '\\')
            {
                isBackSlash = true;
            }
            else if (symbol == '"')
            {
                return sb.ToString();
            }
            else
            {
                sb.Append(symbol);
            }
        }

        throw new JsonEndException("");
    }

    private NumberNode ReadAsNumber()
    {
        var sb = new StringBuilder(32);

        var isExponentialFormat = false;
        var isFloatingPoint = false;
        var isPositiveNumber = true;
        var isPositiveExponential = true;

        while (MoveNext())
        {
            var symbol = CurrentChar;

            switch (symbol)
            {
                case '-' when isPositiveNumber && !isExponentialFormat:
                    isPositiveNumber = false;
                    break;
                case '-' when isPositiveExponential && isExponentialFormat:
                    isPositiveExponential = false;
                    break;
                case '.' when !isFloatingPoint && !isExponentialFormat:
                    isFloatingPoint = true;
                    break;
                case 'e' or 'E' when !isExponentialFormat:
                    isExponentialFormat = true;
                    break;
                case >= '0' and <= '9':
                    break;
                case '}' or ',' or ' ' or '\t' or '\n' or '\r' or ']':
                    return new NumberNode(sb.ToString());
                default:
                    throw new JsonInvalidNumberException($"Symbol '{symbol} is not converted");
            }

            sb.Append(symbol);
        }

        throw new JsonEndException("");
    }

    private NullNode ReadAsNull()
    {
        foreach (var c in JsonConstants.Null)
        {
            MoveNext();
            if (CurrentChar != c)
                throw new JsonInvalidNullException();
        }

        return new NullNode();
    }

    private BoolNode ReadAsBool()
    {
        string readString;
        var symbol = CurrentChar;
        if (JsonConstants.True[0] == symbol)
            readString = JsonConstants.True;
        else if (JsonConstants.False[0] == symbol)
            readString = JsonConstants.False;
        else
            throw new JsonConverterNotSupportedException("It isn't bool!");

        foreach (var c in readString)
        {
            MoveNext();
            symbol = CurrentChar;
            if (c != symbol)
                throw new JsonConverterNotSupportedException("It isn't bool!");
        }

        var boolNode = new BoolNode(readString);

        return boolNode;
    }

    #endregion
}