﻿// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.IRecordEnum
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System.Reflection;

namespace Ported.VisualBasic.CompilerServices
{
  internal interface IRecordEnum
  {
    bool Callback(FieldInfo FieldInfo, ref object Value);
  }
}
