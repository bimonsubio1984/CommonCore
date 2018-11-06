// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.RecycleOption
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Specifies whether a file should be deleted permanently or placed in the Recycle Bin.</summary>
  public enum RecycleOption
  {
    /// <summary>Delete the file or directory permanently. Default.</summary>
    DeletePermanently = 2,
    /// <summary>Send the file or directory to the Recycle Bin.</summary>
    SendToRecycleBin = 3,
  }
}
