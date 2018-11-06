// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Logging.DiskSpaceExhaustedOption
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.Logging
{
  /// <summary>Determines what to do when the <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> object attempts to write to a log and there is less free disk space available than specified by the <see cref="P:Ported.VisualBasic.Logging.FileLogTraceListener.ReserveDiskSpace" /> property.</summary>
  public enum DiskSpaceExhaustedOption
  {
    /// <summary>Throw an exception.</summary>
    ThrowException,
    /// <summary>Discard log messages.</summary>
    DiscardMessages,
  }
}
