// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileAttribute
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;

namespace Ported.VisualBasic
{
  /// <summary>Indicates the file attributes to use when calling file-access functions.</summary>
  [Flags]
  public enum FileAttribute
  {
    /// <summary>Normal (default for <see langword="Dir" /> and <see langword="SetAttr" />). No special characteristics apply to this file. This member is equivalent to the Visual Basic constant <see langword="vbNormal" />.</summary>
    Normal = 0,
    /// <summary>Read only. This member is equivalent to the Visual Basic constant <see langword="vbReadOnly" />.</summary>
    ReadOnly = 1,
    /// <summary>Hidden. This member is equivalent to the Visual Basic constant <see langword="vbHidden" />.</summary>
    Hidden = 2,
    /// <summary>System file. This member is equivalent to the Visual Basic constant <see langword="vbSystem" />.</summary>
    System = 4,
    /// <summary>Volume label. This attribute is not valid when used with <see langword="SetAttr" />. This member is equivalent to the Visual Basic constant <see langword="vbVolume" />.</summary>
    Volume = 8,
    /// <summary>Directory or folder. This member is equivalent to the Visual Basic constant <see langword="vbDirectory" />.</summary>
    Directory = 16, // 0x00000010
    /// <summary>File has changed since last backup. This member is equivalent to the Visual Basic constant <see langword="vbArchive" />.</summary>
    Archive = 32, // 0x00000020
  }
}
