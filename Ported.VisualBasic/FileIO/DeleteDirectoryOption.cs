// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.DeleteDirectoryOption
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Specifies what should be done when a directory that is to be deleted contains files or directories.</summary>
  public enum DeleteDirectoryOption
  {
    /// <summary>Throw an <see cref="T:System.IO.IOException" /> if the directory is not empty. The <see langword="Data" /> property of the exception lists the file(s) that could not be deleted.</summary>
    ThrowIfDirectoryNonEmpty = 4,
    /// <summary>Delete the contents of the directory along with the directory. Default.</summary>
    DeleteAllContents = 5,
  }
}
