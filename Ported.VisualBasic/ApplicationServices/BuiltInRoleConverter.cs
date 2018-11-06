// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.BuiltInRoleConverter
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Security.Principal;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides a type converter to convert <see cref="T:Ported.VisualBasic.ApplicationServices.BuiltInRole" /> enumeration values to <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> enumeration values.</summary>
  [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
  public class BuiltInRoleConverter : TypeConverter
  {
    /// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context. </param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you wish to convert to. </param>
    /// <returns>A <see cref="T:System.Boolean" /> object that indicates whether this converter can perform the conversion.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType != null && destinationType.Equals(typeof (WindowsBuiltInRole)))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    /// <summary>Converts the given object to another type.</summary>
    /// <param name="context">A formatter context. </param>
    /// <param name="culture">The culture into which <paramref name="value" /> will be converted.</param>
    /// <param name="value">The object to convert. </param>
    /// <param name="destinationType">The type to convert the object to. </param>
    /// <returns>The converted object.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationType" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == null || !destinationType.Equals(typeof (WindowsBuiltInRole)))
        return base.ConvertTo(context, culture, value, destinationType);
      User.ValidateBuiltInRoleEnumValue((BuiltInRole) value, nameof (value));
      return (object) this.GetWindowsBuiltInRole(value);
    }

    private WindowsBuiltInRole GetWindowsBuiltInRole(object role)
    {
      object obj = Enum.Parse(typeof (WindowsBuiltInRole), Enum.GetName(typeof (BuiltInRole), role));
      //if (obj != null)
        return (WindowsBuiltInRole) obj;
      //WindowsBuiltInRole windowsBuiltInRole;
      //return windowsBuiltInRole;
    }
  }
}

*/