// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Logging.LogFileCreationScheduleOption
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.Logging
{
  /// <summary>Determines which date to include in the names of the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> class log files.</summary>
  public enum LogFileCreationScheduleOption
  {
    /// <summary>Do not include the date in the log file name.</summary>
    None,
    /// <summary>Include the current date in the log file name.</summary>
    Daily,
    /// <summary>Include the first day of the current week in the log file name.</summary>
    Weekly,
  }
}
