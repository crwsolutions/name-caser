using System.Runtime.CompilerServices;

namespace NameCaser.Utils;

internal static class TypesExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool Has(this Types flags, Types type) => 
        (flags & type) != 0;
}
