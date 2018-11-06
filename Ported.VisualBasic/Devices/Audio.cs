// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Audio
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*
using Ported.VisualBasic.CompilerServices;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Security;
using System.Security.Permissions;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides methods for playing sounds.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Audio
  {
    private SoundPlayer m_Sound;

    /// <summary>Plays a .wav sound file.</summary>
    /// <param name="location">A <see langword="String" /> containing the name of the sound file </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="location" /> is an empty string.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have sufficient permissions to access the file named by <paramref name="location" />.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The file path is malformed in <paramref name="location" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path name in <paramref name="location" /> is too long.</exception>
    /// <exception cref="T:System.Security.SecurityException">A partial-trust situation exists in which the user lacks necessary permissions.</exception>
    public void Play(string location)
    {
      this.Play(location, AudioPlayMode.Background);
    }

    /// <summary>Plays a .wav sound file.</summary>
    /// <param name="location">A <see langword="String" /> containing the name of the sound file </param>
    /// <param name="playMode">
    /// <see cref="T:Ported.VisualBasic.AudioPlayMode" /> mode for playing the sound. By default, <see langword="AudioPlayMode.Background" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="location" /> is an empty string.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have sufficient permissions to access the file named by <paramref name="location" />.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The file path is malformed in <paramref name="location" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path name in <paramref name="location" /> is too long.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="playMode" /> is not one of the <see cref="T:Ported.VisualBasic.AudioPlayMode" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">A partial-trust situation exists in which the user lacks necessary permissions.</exception>
    public void Play(string location, AudioPlayMode playMode)
    {
      this.ValidateAudioPlayModeEnum(playMode, nameof (playMode));
      this.Play(new SoundPlayer(this.ValidateFilename(location)), playMode);
    }

    /// <summary>Plays a .wav sound file.</summary>
    /// <param name="data">
    /// <see langword="Byte" /> array that represents the sound file.</param>
    /// <param name="playMode">
    /// <see cref="T:Ported.VisualBasic.AudioPlayMode" /> mode for playing the sound. By default, <see langword="AudioPlayMode.Background" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="data" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="playMode" /> is not one of the <see cref="T:Ported.VisualBasic.AudioPlayMode" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">A partial-trust situation exists in which the user lacks necessary permissions.</exception>
    public void Play(byte[] data, AudioPlayMode playMode)
    {
      if (data == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (data));
      this.ValidateAudioPlayModeEnum(playMode, nameof (playMode));
      MemoryStream memoryStream = new MemoryStream(data);
      this.Play((Stream) memoryStream, playMode);
      memoryStream.Close();
    }

    /// <summary>Plays a .wav sound file.</summary>
    /// <param name="stream">
    /// <see cref="T:System.IO.Stream" /> that represents the sound file.</param>
    /// <param name="playMode">
    /// <see cref="T:Ported.VisualBasic.AudioPlayMode" /> mode for playing the sound. By default, <see langword="AudioPlayMode.Background" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="playMode" /> is not one of the <see cref="T:Ported.VisualBasic.AudioPlayMode" /> enumeration values.</exception>
    /// <exception cref="T:System.Security.SecurityException">A partial-trust situation exists in which the user lacks necessary permissions.</exception>
    public void Play(Stream stream, AudioPlayMode playMode)
    {
      this.ValidateAudioPlayModeEnum(playMode, nameof (playMode));
      if (stream == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (stream));
      this.Play(new SoundPlayer(stream), playMode);
    }

    /// <summary>Plays a system sound.</summary>
    /// <param name="systemSound">
    /// <see cref="T:System.Media.SystemSound" /> object representing the system sound to play.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="systemSound" /> is <see langword="Nothing" />.</exception>
    public void PlaySystemSound(SystemSound systemSound)
    {
      if (systemSound == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (systemSound));
      systemSound.Play();
    }

    /// <summary>Stops a sound playing in the background.</summary>
    public void Stop()
    {
      Audio.InternalStop(new SoundPlayer());
    }

    private void Play(SoundPlayer sound, AudioPlayMode mode)
    {
      if (this.m_Sound != null)
        Audio.InternalStop(this.m_Sound);
      this.m_Sound = sound;
      switch (mode)
      {
        case AudioPlayMode.WaitToComplete:
          this.m_Sound.PlaySync();
          break;
        case AudioPlayMode.Background:
          this.m_Sound.Play();
          break;
        case AudioPlayMode.BackgroundLoop:
          this.m_Sound.PlayLooping();
          break;
      }
    }

    private static void InternalStop(SoundPlayer sound)
    {
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      try
      {
        sound.Stop();
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
    }

    private string ValidateFilename(string location)
    {
      if (Operators.CompareString(location, "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (location));
      return location;
    }

    private void ValidateAudioPlayModeEnum(AudioPlayMode value, string paramName)
    {
      switch (value)
      {
        case AudioPlayMode.WaitToComplete:
        case AudioPlayMode.Background:
        case AudioPlayMode.BackgroundLoop:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (AudioPlayMode));
      }
    }
  }
}

*/