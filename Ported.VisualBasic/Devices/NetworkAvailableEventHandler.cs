// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.NetworkAvailableEventHandler
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System.ComponentModel;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Represents the method that will handle the <see langword="My.Application.NetworkAvailabilityChanged" /> or <see langword="My.Computer.Network.NetworkAvailabilityChanged" /> event.</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">A <see cref="T:Ported.VisualBasic.Devices.NetworkAvailableEventArgs" /> object that contains the event data.</param>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public delegate void NetworkAvailableEventHandler(object sender, NetworkAvailableEventArgs e);
}
