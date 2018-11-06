// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Logging.LogFileLocation
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.Logging
{
  /// <summary>Determines which predefined path the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> class uses to write its log files.</summary>
  public enum LogFileLocation
  {
    /// <summary>Use the path of the current system's temporary folder.</summary>
    TempDirectory,
    /// <summary>Use the path for a user's application data.</summary>
    LocalUserApplicationDirectory,
    /// <summary>Use the path for the application data that is shared among all users.</summary>
    CommonApplicationDirectory,
    /// <summary>Use the path for the executable file that started the application.</summary>
    ExecutableDirectory,
    /// <summary>If the string specified by <see cref="P:Ported.VisualBasic.Logging.FileLogTraceListener.CustomLocation" /> is not empty, then use it as the path. Otherwise, use the path for a user's application data.</summary>
    Custom,
  }
}
