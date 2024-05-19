namespace NameCaser.Utils;

internal struct CharBuilder(int size)
{
    int _index = -1;
    private readonly char[] _chars = new char[size];

    internal void Append(char c) => _chars[++_index] = c;

    public override readonly string ToString() => new(_chars);
}
