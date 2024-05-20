using System.Runtime.CompilerServices;

namespace NameCaser.Core;

internal static class TypesExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool Has(this Types flags, Types type) =>
        (flags & type) != 0;
}
