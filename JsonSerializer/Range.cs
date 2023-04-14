namespace JsonSerializer;

public struct Range
{
    public int start;
    public int end;

    public Range(int start, int end)
    {
        if (start > end) throw new ArgumentException();

        this.start = start;
        this.end = end;
    }

    public override string ToString()
    {
        return $"({start}; {end})";
    }
}