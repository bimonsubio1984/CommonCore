// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.VbStrConv
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;

namespace Ported.VisualBasic
{
  /// <summary>Indicates which type of conversion to perform when calling the <see langword="StrConv" /> function.</summary>
  [Flags]
  public enum VbStrConv
  {
    /// <summary>Performs no conversion.</summary>
    None = 0,
    /// <summary>Converts the string to uppercase characters. This member is equivalent to the Visual Basic constant <see langword="vbUpperCase" />.</summary>
    Uppercase = 1,
    /// <summary>Converts the string to lowercase characters. This member is equivalent to the Visual Basic constant <see langword="vbLowerCase" />.</summary>
    Lowercase = 2,
    /// <summary>Converts the first letter of every word in the string to uppercase. This member is equivalent to the Visual Basic constant <see langword="vbProperCase" />.</summary>
    ProperCase = Lowercase | Uppercase, // 0x00000003
    /// <summary>Converts narrow (single-byte) characters in the string to wide (double-byte) characters. Applies to Asian locales. This member is equivalent to the Visual Basic constant <see langword="vbWide" />.</summary>
    Wide = 4,
    /// <summary>Converts wide (double-byte) characters in the string to narrow (single-byte) characters. Applies to Asian locales. This member is equivalent to the Visual Basic constant <see langword="vbNarrow" />.</summary>
    Narrow = 8,
    /// <summary>Converts Hiragana characters in the string to Katakana characters. Applies to Japanese locale only. This member is equivalent to the Visual Basic constant <see langword="vbKatakana" />.</summary>
    Katakana = 16, // 0x00000010
    /// <summary>Converts Katakana characters in the string to Hiragana characters. Applies to Japanese locale only. This member is equivalent to the Visual Basic constant <see langword="vbHiragana" />.</summary>
    Hiragana = 32, // 0x00000020
    /// <summary>Converts the string to Simplified Chinese characters. This member is equivalent to the Visual Basic constant <see langword="vbSimplifiedChinese" />.</summary>
    SimplifiedChinese = 256, // 0x00000100
    /// <summary>Converts the string to Traditional Chinese characters. This member is equivalent to the Visual Basic constant <see langword="vbTraditionalChinese" />.</summary>
    TraditionalChinese = 512, // 0x00000200
    /// <summary>Converts the string from file system rules for casing to linguistic rules. This member is equivalent to the Visual Basic constant <see langword="vbLinguisticCasing" />.</summary>
    LinguisticCasing = 1024, // 0x00000400
  }
}
