// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.NetworkAvailableEventArgs
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides data for the <see langword="My.Application.NetworkAvailabilityChanged" /> and <see langword="My.Computer.Network.NetworkAvailabilityChanged" /> events. </summary>
  public class NetworkAvailableEventArgs : EventArgs
  {
    private bool m_NetworkAvailable;

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs" /> class.</summary>
    /// <param name="networkAvailable">A <see cref="T:System.Boolean" /> that indicates whether a network is available to the application.</param>
    public NetworkAvailableEventArgs(bool networkAvailable)
    {
      this.m_NetworkAvailable = networkAvailable;
    }

    /// <summary>Gets a value indicating whether a network is available to the application.</summary>
    /// <returns>A <see cref="T:System.Boolean" /> that indicates whether a network is available to the application.</returns>
    public bool IsNetworkAvailable
    {
      get
      {
        return this.m_NetworkAvailable;
      }
    }
  }
}
