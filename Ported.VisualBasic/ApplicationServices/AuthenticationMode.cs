// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.AuthenticationMode
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Indicates how a Visual Basic application authenticates the user for the <see langword="My.User" /> object.</summary>
  public enum AuthenticationMode
  {
    /// <summary>The <see cref="M:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.#ctor(Ported.VisualBasic.ApplicationServices.AuthenticationMode)" /> constructor initializes the principal for the application's main thread with the current user's Windows user information. </summary>
    Windows,
    /// <summary>The <see cref="M:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.#ctor(Ported.VisualBasic.ApplicationServices.AuthenticationMode)" /> constructor does not initialize the principal for the application's main thread. The application needs to initialize the principal for the application's main thread.</summary>
    ApplicationDefined,
  }
}
