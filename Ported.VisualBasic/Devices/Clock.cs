// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Clock
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Security.Permissions;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides properties for accessing the current local time and Universal Coordinated Time (equivalent to Greenwich Mean Time) from the system clock.</summary>
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Clock
  {
    /// <summary>Gets a <see langword="Date" /> object that contains the current local date and time on this computer.</summary>
    /// <returns>A <see langword="Date" /> object that contains the current local date and time.</returns>
    public DateTime LocalTime
    {
      get
      {
        return DateTime.Now;
      }
    }

    /// <summary>Gets a <see langword="Date" /> object that contains the current local date and time on the computer, expressed as a UTC (GMT) time.</summary>
    /// <returns>A <see langword="Date" /> object that contains the current date and time expressed as UTC (GMT) time.</returns>
    public DateTime GmtTime
    {
      get
      {
        return DateTime.UtcNow;
      }
    }

    /// <summary>Gets the millisecond count from the computer's system timer.</summary>
    /// <returns>An <see langword="Integer" /> containing the millisecond count from the computer's system timer.</returns>
    public int TickCount
    {
      get
      {
        return Environment.TickCount;
      }
    }
  }
}
