// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.HideModuleNameAttribute
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="HideModuleNameAttribute" /> attribute, when applied to a module, allows the module members to be accessed using only the qualification needed for the module.</summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public sealed class HideModuleNameAttribute : Attribute
  {
  }
}
