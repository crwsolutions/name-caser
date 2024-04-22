internal struct StructBuilder
{
    int _index = -1;
    readonly char[] _chars;
    public StructBuilder(int size)
    {
        _chars = new char[size];
    }

    public void Append(char c)
    {
        _index++;
        _chars[_index] = c;
    }

    public override string ToString()
    {
        return new string(_chars[0.._index]);
    }
}