// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VT
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.CompilerServices
{
  internal enum VT : short
  {
    Empty = 0,
    DBNull = 1,
    Short = 2,
    Integer = 3,
    Single = 4,
    Double = 5,
    Currency = 6,
    Date = 7,
    String = 8,
    Error = 10, // 0x000A
    Boolean = 11, // 0x000B
    Variant = 12, // 0x000C
    Decimal = 14, // 0x000E
    Byte = 17, // 0x0011
    Char = 18, // 0x0012
    Long = 20, // 0x0014
    Structure = 36, // 0x0024
    Array = 8192, // 0x2000
    ByteArray = 8209, // 0x2011
    CharArray = 8210, // 0x2012
  }
}
