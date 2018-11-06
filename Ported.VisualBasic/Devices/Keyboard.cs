// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Keyboard
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System.Security.Permissions;
using System.Windows.Forms;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides properties for accessing the current state of the keyboard, such as what keys are currently pressed, and provides a method to send keystrokes to the active window.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Keyboard
  {
    /// <summary>Sends one or more keystrokes to the active window, as if typed on the keyboard.</summary>
    /// <param name="keys">A <see langword="String" /> that defines the keys to send.</param>
    /// <exception cref="T:System.Security.SecurityException">A partial-trust situation exists in which the user lacks necessary permissions.</exception>
    public void SendKeys(string keys)
    {
      this.SendKeys(keys, false);
    }

    /// <summary>Sends one or more keystrokes to the active window, as if typed on the keyboard.</summary>
    /// <param name="keys">A <see langword="String" /> that defines the keys to send.</param>
    /// <param name="wait">Optional. A <see langword="Boolean" /> that specifies whether or not to wait for keystrokes to get processed before the application continues. <see langword="True" /> by default.</param>
    /// <exception cref="T:System.Security.SecurityException">A partial-trust situation exists in which the user lacks necessary permissions.</exception>
    public void SendKeys(string keys, bool wait)
    {
        throw new System.Exception("Not implemented!");
    }

    /// <summary>Gets a <see langword="Boolean" /> indicating if a SHIFT key is down.</summary>
    /// <returns>A <see langword="Boolean" /> value. <see langword="True" /> if a SHIFT key is down; otherwise <see langword="False" />.</returns>
    public bool ShiftKeyDown
    {
      get
      {
        return (uint) (Control.ModifierKeys & Keys.Shift) > 0U;
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> indicating if the ALT key is down.</summary>
    /// <returns>A <see langword="Boolean" /> value: <see langword="True" /> if the ALT key is down; otherwise <see langword="False" />.</returns>
    public bool AltKeyDown
    {
      get
      {
        return (uint) (Control.ModifierKeys & Keys.Alt) > 0U;
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> indicating if a CTRL key is down.</summary>
    /// <returns>A <see langword="Boolean" /> value. <see langword="True" /> if a CTRL key is down; otherwise <see langword="False" />.</returns>
    public bool CtrlKeyDown
    {
      get
      {
        return (uint) (Control.ModifierKeys & Keys.Control) > 0U;
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> indicating if CAPS LOCK is turned on. </summary>
    /// <returns>A <see langword="Boolean" /> value: <see langword="True" /> if CAPS LOCK is turned on; otherwise <see langword="False" />.</returns>
    public bool CapsLock
    {
      get
      {
        return ((uint) Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.GetKeyState(20) & 1U) > 0U;
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> indicating if the NUM LOCK key is on. </summary>
    /// <returns>A <see langword="Boolean" /> value. <see langword="True" /> if NUM LOCK is on; otherwise <see langword="False" />.</returns>
    public bool NumLock
    {
      get
      {
        return ((uint) Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.GetKeyState(144) & 1U) > 0U;
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> indicating whether the SCROLL LOCK key is on. </summary>
    /// <returns>A <see langword="Boolean" /> value. <see langword="True" /> if SCROLL LOCK is on; otherwise <see langword="False" />.</returns>
    public bool ScrollLock
    {
      get
      {
        return ((uint) Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.GetKeyState(145) & 1U) > 0U;
      }
    }
  }
}

*/