// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.AudioPlayMode
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates how to play sounds when calling audio methods.</summary>
  public enum AudioPlayMode
  {
    /// <summary>Causes the <see langword="My.Computer.Audio.Play" /> method to play the sound, and waits until it completes before calling code continues.</summary>
    WaitToComplete,
    /// <summary>Causes the <see langword="My.Computer.Audio.Play" /> method to play the sound in the background. The calling code continues to execute.</summary>
    Background,
    /// <summary>Causes the <see langword="My.Computer.Audio.Play" /> method to play the sound in the background until the <see cref="M:Ported.VisualBasic.Devices.Audio.Stop" /> method is called. The calling code continues to execute.</summary>
    BackgroundLoop,
  }
}
