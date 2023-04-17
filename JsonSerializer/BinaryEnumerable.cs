

using System.Collections;
using JsonSerializer.Exeptions;

namespace JsonSerializer;



public class BinaryEnumerable : IEnumerator<char>
{
    private readonly BinaryReader _binaryReader;
    private char _current = char.MaxValue;

    public BinaryEnumerable(BinaryReader reader)
    {
        _binaryReader = reader
                        ?? throw new ArgumentNullException(nameof(reader));
    }

    public BinaryEnumerable(Stream stream)
        : this(new BinaryReader(stream))
    {
    }

    public bool MoveNext()
    {
        var value = _binaryReader.PeekChar();

        if (value == -1)
        {
            if (_current == char.MaxValue)
                throw new JsonEndException("");

            return false;
        }

        _current = _binaryReader.ReadChar();

        return true;
    }

    public void Reset()
    {
    }

    public char Current
    {
        get
        {
            if (_current == char.MaxValue)
                throw new JsonEndException("");

            return _current;
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _binaryReader.Dispose();
    }

    public void Back()
    {
        _binaryReader.BaseStream.Position--;
        _current = (char)_binaryReader.PeekChar();
    }

    public IEnumerator<char> GetEnumerator()
    {
        return this;
    }
}