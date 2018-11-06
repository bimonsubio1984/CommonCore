// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.VariantType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates the type of a variant object, returned by the <see langword="VarType" /> function.</summary>
  public enum VariantType
  {
    /// <summary>Null reference. This member is equivalent to the Visual Basic constant <see langword="vbEmpty" />.</summary>
    Empty = 0,
    /// <summary>Null object. This member is equivalent to the Visual Basic constant <see langword="vbNull" />.</summary>
    Null = 1,
    /// <summary>
    /// <see langword="Short" />. (-32,768 through 32,767.)</summary>
    Short = 2,
    /// <summary>
    /// <see langword="Integer" />. (-2,147,483,648 through 2,147,483,647.) This member is equivalent to the Visual Basic constant <see langword="vbInteger" />.</summary>
    Integer = 3,
    /// <summary>
    /// <see langword="Single" />. (-3.402823E+38 through -1.401298E-45 for negative values; 1.401298E-45 through 3.402823E+38 for positive values.) This member is equivalent to the Visual Basic constant <see langword="vbSingle" />.</summary>
    Single = 4,
    /// <summary>
    /// <see langword="Double" />. (-1.79769313486231E+308 through -4.94065645841247E-324 for negative values; 4.94065645841247E-324 through 1.79769313486231E+308 for positive values.) This member is equivalent to the Visual Basic constant <see langword="vbDouble" />.</summary>
    Double = 5,
    /// <summary>Currency. This member is equivalent to the Visual Basic constant <see langword="vbCurrency" />.</summary>
    Currency = 6,
    /// <summary>
    /// <see langword="Date" />. (0:00:00 on January 1, 0001 through 11:59:59 PM on December 31, 9999.) This member is equivalent to the Visual Basic constant <see langword="vbDate" />.</summary>
    Date = 7,
    /// <summary>
    /// <see langword="String" />. (0 to approximately 2 billion Unicode characters.) This member is equivalent to the Visual Basic constant <see langword="vbString" />.</summary>
    String = 8,
    /// <summary>Any type can be stored in a variable of type <see langword="Object" />. This member is equivalent to the Visual Basic constant <see langword="vbObject" />.</summary>
    Object = 9,
    /// <summary>
    ///   <see cref="T:System.Exception" />
    /// </summary>
    Error = 10, // 0x0000000A
    /// <summary>
    /// <see langword="Boolean" />. (<see langword="True" /> or <see langword="False" />.) This member is equivalent to the Visual Basic constant <see langword="vbBoolean" />.</summary>
    Boolean = 11, // 0x0000000B
    /// <summary>
    /// <see langword="Variant" />. This member is equivalent to the Visual Basic constant <see langword="vbVariant" />.</summary>
    Variant = 12, // 0x0000000C
    /// <summary>DataObject.</summary>
    DataObject = 13, // 0x0000000D
    /// <summary>
    /// <see langword="Decimal" />. (0 through +/-79,228,162,514,264,337,593,543,950,335 with no decimal point; 0 through +/-7.9228162514264337593543950335 with 28 places to the right of the decimal; smallest non-zero number is +/-0.0000000000000000000000000001.) This member is equivalent to the Visual Basic constant <see langword="vbDecimal" />.</summary>
    Decimal = 14, // 0x0000000E
    /// <summary>
    /// <see langword="Byte" />. (0 through 255.) This member is equivalent to the Visual Basic constant <see langword="vbByte" />.</summary>
    Byte = 17, // 0x00000011
    /// <summary>
    /// <see langword="Char" />. (0 through 65535.) This member is equivalent to the Visual Basic constant <see langword="vbChar" />.</summary>
    Char = 18, // 0x00000012
    /// <summary>
    /// <see langword="Long" />. (-9,223,372,036,854,775,808 through 9,223,372,036,854,775,807.) This member is equivalent to the Visual Basic constant <see langword="vbLong" />.</summary>
    Long = 20, // 0x00000014
    /// <summary>User-defined type. Each member of the structure has a range determined by its data type and independent of the ranges of the other members. This member is equivalent to the Visual Basic constant <see langword="vbUserDefinedType" />.</summary>
    UserDefinedType = 36, // 0x00000024
    /// <summary>Array. This member is equivalent to the Visual Basic constant <see langword="vbArray" />.</summary>
    Array = 8192, // 0x00002000
  }
}
