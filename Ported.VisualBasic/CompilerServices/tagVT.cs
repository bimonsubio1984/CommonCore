// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.tagVT
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.CompilerServices
{
  internal enum tagVT : short
  {
    VT_RESERVED = -32768, // -0x8000
    VT_ILLEGAL = -1,
    VT_EMPTY = 0,
    VT_NULL = 1,
    VT_I2 = 2,
    VT_I4 = 3,
    VT_R4 = 4,
    VT_R8 = 5,
    VT_CY = 6,
    VT_DATE = 7,
    VT_BSTR = 8,
    VT_DISPATCH = 9,
    VT_ERROR = 10, // 0x000A
    VT_BOOL = 11, // 0x000B
    VT_VARIANT = 12, // 0x000C
    VT_UNKNOWN = 13, // 0x000D
    VT_DECIMAL = 14, // 0x000E
    VT_I1 = 16, // 0x0010
    VT_UI1 = 17, // 0x0011
    VT_UI2 = 18, // 0x0012
    VT_UI4 = 19, // 0x0013
    VT_I8 = 20, // 0x0014
    VT_UI8 = 21, // 0x0015
    VT_INT = 22, // 0x0016
    VT_UINT = 23, // 0x0017
    VT_VOID = 24, // 0x0018
    VT_HRESULT = 25, // 0x0019
    VT_PTR = 26, // 0x001A
    VT_SAFEARRAY = 27, // 0x001B
    VT_CARRAY = 28, // 0x001C
    VT_USERDEFINED = 29, // 0x001D
    VT_LPSTR = 30, // 0x001E
    VT_LPWSTR = 31, // 0x001F
    VT_RECORD = 36, // 0x0024
    VT_FILETIME = 64, // 0x0040
    VT_BLOB = 65, // 0x0041
    VT_STREAM = 66, // 0x0042
    VT_STORAGE = 67, // 0x0043
    VT_STREAMED_OBJECT = 68, // 0x0044
    VT_STORED_OBJECT = 69, // 0x0045
    VT_BLOB_OBJECT = 70, // 0x0046
    VT_CF = 71, // 0x0047
    VT_CLSID = 72, // 0x0048
    VT_BSTR_BLOB = 4095, // 0x0FFF
    VT_ILLEGALMASKED = 4095, // 0x0FFF
    VT_TYPEMASK = 4095, // 0x0FFF
    VT_VECTOR = 4096, // 0x1000
    VT_ARRAY = 8192, // 0x2000
    VT_BYREF = 16384, // 0x4000
  }
}
