// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Globals
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Globals" /> module contains script engine functions. </summary>
  [StandardModule]
  public sealed class Globals
  {
    /// <summary>Returns a <see langword="String" /> representing the runtime currently in use.</summary>
    /// <returns>Returns a <see langword="String" /> representing the runtime currently in use.</returns>
    public static string ScriptEngine
    {
      get
      {
        return "VB";
      }
    }

    /// <summary>Returns an <see langword="Integer" /> containing the major version number of the runtime currently in use.</summary>
    /// <returns>Returns an <see langword="Integer" /> containing the major version number of the runtime currently in use.</returns>
    public static int ScriptEngineMajorVersion
    {
      get
      {
        return 8;
      }
    }

    /// <summary>Returns an <see langword="Integer" /> containing the minor version number of the runtime currently in use.</summary>
    /// <returns>Returns an <see langword="Integer" /> containing the minor version number of the runtime currently in use.</returns>
    public static int ScriptEngineMinorVersion
    {
      get
      {
        return 0;
      }
    }

    /// <summary>Returns an <see langword="Integer" /> containing the build version number of the runtime currently in use.</summary>
    /// <returns>Returns an <see langword="Integer" /> containing the build version number of the runtime currently in use.</returns>
    public static int ScriptEngineBuildVersion
    {
      get
      {
        return 50727;
      }
    }
  }
}
