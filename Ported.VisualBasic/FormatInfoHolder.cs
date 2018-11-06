// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FormatInfoHolder
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Globalization;

namespace Ported.VisualBasic
{
  internal sealed class FormatInfoHolder : IFormatProvider
  {
    private NumberFormatInfo nfi;

    internal FormatInfoHolder(NumberFormatInfo nfi)
    {
      this.nfi = nfi;
    }

    object IFormatProvider.GetFormat(Type service)
    {
      if (service == typeof (NumberFormatInfo))
        return (object) this.nfi;
      throw new ArgumentException(Utils.GetResourceString("InternalError"));
    }
  }
}
