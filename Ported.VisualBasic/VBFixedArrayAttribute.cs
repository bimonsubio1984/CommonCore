// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.VBFixedArrayAttribute
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;

namespace Ported.VisualBasic
{
  /// <summary>Indicates that an array in a structure or non-local variable should be treated as a fixed-length array.</summary>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
  public sealed class VBFixedArrayAttribute : Attribute
  {
    internal int FirstBound;
    internal int SecondBound;

    /// <summary>Returns the bounds of the array.</summary>
    /// <returns>Contains an integer array that represents the bounds of the array.</returns>
    public int[] Bounds
    {
      get
      {
        if (this.SecondBound == -1)
          return new int[1]{ this.FirstBound };
        return new int[2]
        {
          this.FirstBound,
          this.SecondBound
        };
      }
    }

    /// <summary>Returns the size of the array.</summary>
    /// <returns>Contains an integer that represents the number of elements in the array.</returns>
    public int Length
    {
      get
      {
        if (this.SecondBound == -1)
          return checked (this.FirstBound + 1);
        return checked (this.FirstBound + 1 * this.SecondBound + 1);
      }
    }

    /// <summary>Initializes the value of the <see langword="Bounds" /> property.</summary>
    /// <param name="UpperBound1">Initializes the value of upper field, which represents the size of the first dimension of the array.</param>
    public VBFixedArrayAttribute(int UpperBound1)
    {
      if (UpperBound1 < 0)
        throw new ArgumentException(Utils.GetResourceString("Invalid_VBFixedArray"));
      this.FirstBound = UpperBound1;
      this.SecondBound = -1;
    }

    /// <summary>Initializes the value of the <see langword="Bounds" /> property.</summary>
    /// <param name="UpperBound1">Initializes the value of upper field, which represents the size of the first dimension of the array.</param>
    /// <param name="UpperBound2">Initializes the value of upper field, which represents the size of the second dimension of the array.</param>
    public VBFixedArrayAttribute(int UpperBound1, int UpperBound2)
    {
      if (UpperBound1 < 0 || UpperBound2 < 0)
        throw new ArgumentException(Utils.GetResourceString("Invalid_VBFixedArray"));
      this.FirstBound = UpperBound1;
      this.SecondBound = UpperBound2;
    }
  }
}
