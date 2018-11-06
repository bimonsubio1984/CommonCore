// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.DateType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class DateType
  {
    private DateType()
    {
    }

    /// <summary>Returns a <see langword="Date" /> value that corresponds to the specified string.</summary>
    /// <param name="Value">Required. String to convert to a <see langword="Date" /> value.</param>
    /// <returns>The <see langword="Date" /> value that corresponds to <paramref name="Value" />.</returns>
    public static DateTime FromString(string Value)
    {
      return DateType.FromString(Value, Utils.GetCultureInfo());
    }

    /// <summary>Returns a <see langword="Date" /> value that corresponds to the specified string and culture information. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Date" /> value.</param>
    /// <param name="culture">Required. A <see cref="T:System.Globalization.CultureInfo" /> object that defines how date values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="Date" /> value that corresponds to <paramref name="Value" />.</returns>
    public static DateTime FromString(string Value, CultureInfo culture)
    {
      DateTime Result= DateTime.MinValue;
      if (DateType.TryParse(Value, ref Result))
        return Result;
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Date"));
    }

    /// <summary>Returns a <see langword="Date" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Date" /> value.</param>
    /// <returns>The <see langword="Date" /> value that corresponds to <paramref name="Value" />.</returns>
    public static DateTime FromObject(object Value)
    {
      if (Value != null)
      {
        IConvertible convertible = Value as IConvertible;
        if (convertible != null)
        {
          switch (convertible.GetTypeCode())
          {
            case TypeCode.DateTime:
              return convertible.ToDateTime((IFormatProvider) null);
            case TypeCode.String:
              return DateType.FromString(convertible.ToString((IFormatProvider) null), Utils.GetCultureInfo());
          }
        }
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Date"));
      }
      DateTime dateTime= DateTime.MinValue; 
      return dateTime;
    }

    internal static bool TryParse(string Value, ref DateTime Result)
    {
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      return DateTime.TryParse(Utils.ToHalfwidthNumbers(Value, cultureInfo), (IFormatProvider) cultureInfo, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault, out Result);
    }
  }
}
