// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.VBFixedStringAttribute
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;

namespace Ported.VisualBasic
{
  /// <summary>Indicates that a string should be treated as if it were fixed length.</summary>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
  public sealed class VBFixedStringAttribute : Attribute
  {
    private int m_Length;

    /// <summary>Gets the length of the string.</summary>
    /// <returns>Returns the length of the string.</returns>
    public int Length
    {
      get
      {
        return this.m_Length;
      }
    }

    /// <summary>Initializes the value of the <see langword="SizeConst" /> field.</summary>
    /// <param name="Length">The length of the fixed string.</param>
    public VBFixedStringAttribute(int Length)
    {
      if (Length < 1 || Length > (int) short.MaxValue)
        throw new ArgumentException(Utils.GetResourceString("Invalid_VBFixedString"));
      this.m_Length = Length;
    }
  }
}
