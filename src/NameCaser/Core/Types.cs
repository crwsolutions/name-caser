using System;

namespace NameCaser.Core;

[Flags]
internal enum Types : byte
{
    Lower = 0,              // 0000 0000
    Upper = 1 << 0,         // 0000 0001
    Break = 1 << 1,         // 0000 0010
    Abbreviation = 1 << 2,  // 0000 0100
    First = 1 << 3          // 0000 1000
}
