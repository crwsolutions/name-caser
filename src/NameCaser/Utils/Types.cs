using System;

namespace NameCaser.Utils;

[Flags]
internal enum Types : byte
{
    Lower = 0,       // 0000 0000
    Upper = 1 << 0,  // 0000 0001
    Break = 1 << 1   // 0000 0010
}
