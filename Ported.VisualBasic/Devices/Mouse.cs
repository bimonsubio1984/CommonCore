// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Mouse
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides properties for getting information about the format and configuration of the mouse installed on the local computer.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Mouse
  {
    /// <summary>Gets a <see langword="Boolean" /> that indicates if the functionality of the left and right mouse buttons has been swapped.</summary>
    /// <returns>A <see langword="Boolean" /> with a value <see langword="True" /> if the functionality of the left and right mouse buttons has been swapped; otherwise, <see langword="False" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">The computer has no mouse installed.</exception>
    public bool ButtonsSwapped
    {
      get
      {
        if (SystemInformation.MousePresent)
          return SystemInformation.MouseButtonsSwapped;
        throw ExceptionUtils.GetInvalidOperationException("Mouse_NoMouseIsPresent");
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> that indicates if the mouse has a scroll wheel.</summary>
    /// <returns>A Boolean with value <see langword="True" /> if the mouse has a scroll wheel; otherwise <see langword="False" />.</returns>
    /// <exception cref="T:System.InvalidOperationException">The computer has no mouse installed.</exception>
    public bool WheelExists
    {
      get
      {
        if (SystemInformation.MousePresent)
          return SystemInformation.MouseWheelPresent;
        throw ExceptionUtils.GetInvalidOperationException("Mouse_NoMouseIsPresent");
      }
    }

    /// <summary>Gets a number that indicates how much to scroll when the mouse wheel is rotated one notch.</summary>
    /// <returns>An <see langword="Integer" /> that indicates how much to scroll when the mouse wheel is rotated one notch. A positive value indicates scrolling by that number of lines, while a negative value indicates scrolling by one screen at a time.</returns>
    /// <exception cref="T:System.InvalidOperationException">The computer has no mouse installed.</exception>
    /// <exception cref="T:System.InvalidOperationException">The mouse has no scroll wheel.</exception>
    public int WheelScrollLines
    {
      get
      {
        if (this.WheelExists)
          return SystemInformation.MouseWheelScrollLines;
        throw ExceptionUtils.GetInvalidOperationException("Mouse_NoWheelIsPresent");
      }
    }
  }
}

*/