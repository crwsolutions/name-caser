namespace NameCaserBenchmark;

internal struct StructBuilder(int size)
{
    int _index = -1;
    readonly char[] _chars = new char[size];

    public void Append(char c)
    {
        _index++;
        _chars[_index] = c;
    }

    public override readonly string ToString()
    {
        return new string(_chars[0.._index]);
    }
}