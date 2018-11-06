// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Computer
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*
using Ported.VisualBasic.MyServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides properties for manipulating computer components such as audio, the clock, the keyboard, the file system, and so on.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Computer : ServerComputer
  {
    private Audio m_Audio;
    private Ports m_Ports;
    private static ClipboardProxy m_Clipboard;
    private static Mouse m_Mouse;
    private static Keyboard m_KeyboardInstance;

    /// <summary>Gets an object that provides properties for methods for playing sounds.</summary>
    /// <returns>The <see langword="My.Computer.Audio" /> object for the computer.</returns>
    public Audio Audio
    {
      get
      {
        if (this.m_Audio != null)
          return this.m_Audio;
        this.m_Audio = new Audio();
        return this.m_Audio;
      }
    }

    /// <summary>Gets an object that provides methods for manipulating the Clipboard.</summary>
    /// <returns>The <see langword="My.Computer.Clipboard" /> object for the computer.</returns>
    public ClipboardProxy Clipboard
    {
      get
      {
        if (Computer.m_Clipboard == null)
          Computer.m_Clipboard = new ClipboardProxy();
        return Computer.m_Clipboard;
      }
    }

    /// <summary>Gets an object that provides a property and a method for accessing the computer's serial ports.</summary>
    /// <returns>The <see langword="My.Computer.Ports" /> object.</returns>
    public Ports Ports
    {
      get
      {
        if (this.m_Ports == null)
          this.m_Ports = new Ports();
        return this.m_Ports;
      }
    }

    /// <summary>Gets an object that provides properties for getting information about the format and configuration of the mouse installed on the local computer.</summary>
    /// <returns>The <see langword="My.Computer.Mouse" /> object for the computer.</returns>
    public Mouse Mouse
    {
      get
      {
        if (Computer.m_Mouse != null)
          return Computer.m_Mouse;
        Computer.m_Mouse = new Mouse();
        return Computer.m_Mouse;
      }
    }

    /// <summary>Gets an object that provides properties for accessing the current state of the keyboard, such as what keys are currently pressed, and provides a method to send keystrokes to the active window.</summary>
    /// <returns>The <see langword="My.Computer.Keyboard" /> object for the computer.</returns>
    public Keyboard Keyboard
    {
      get
      {
        if (Computer.m_KeyboardInstance != null)
          return Computer.m_KeyboardInstance;
        Computer.m_KeyboardInstance = new Keyboard();
        return Computer.m_KeyboardInstance;
      }
    }

    /// <summary>Gets the <see cref="T:System.Windows.Forms.Screen" /> object that represents the computer's primary display screen.</summary>
    /// <returns>A <see cref="T:System.Windows.Forms.Screen" /> object that represents the computer's primary screen.</returns>
    public Screen Screen
    {
      get
      {
        return Screen.PrimaryScreen;
      }
    }
  }
}
*/