// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.Ports
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Security.Permissions;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides a property and a method for accessing the computer's serial ports.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class Ports
  {
    /// <summary>Creates and opens a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
    /// <param name="portName">
    /// <see langword="String" />. Required. Name of the port to open.</param>
    /// <returns>An open <see cref="T:System.IO.Ports.SerialPort" /> object, configured with the supplied arguments.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="portName" /> is <see langword="Nothing" /> or an empty string.</exception>
    public SerialPort OpenSerialPort(string portName)
    {
      SerialPort serialPort = new SerialPort(portName);
      serialPort.Open();
      return serialPort;
    }

    /// <summary>Creates and opens a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
    /// <param name="portName">
    /// <see langword="String" />. Required. Name of the port to open.</param>
    /// <param name="baudRate">
    /// <see langword="Integer" />. Baud rate of the port.</param>
    /// <returns>An open <see cref="T:System.IO.Ports.SerialPort" /> object, configured with the supplied arguments.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="portName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="baudRate" /> is less than or equal to zero.</exception>
    public SerialPort OpenSerialPort(string portName, int baudRate)
    {
      SerialPort serialPort = new SerialPort(portName, baudRate);
      serialPort.Open();
      return serialPort;
    }

    /// <summary>Creates and opens a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
    /// <param name="portName">
    /// <see langword="String" />. Required. Name of the port to open.</param>
    /// <param name="baudRate">
    /// <see langword="Integer" />. Baud rate of the port.</param>
    /// <param name="parity">
    /// <see cref="T:System.IO.Ports.Parity" />. Parity of the port.</param>
    /// <returns>An open <see cref="T:System.IO.Ports.SerialPort" /> object, configured with the supplied arguments.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="portName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="baudRate" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="parity" /> is not one of the <see cref="T:System.IO.Ports.Parity" /> enumeration values.</exception>
    public SerialPort OpenSerialPort(string portName, int baudRate, Parity parity)
    {
      SerialPort serialPort = new SerialPort(portName, baudRate, parity);
      serialPort.Open();
      return serialPort;
    }

    /// <summary>Creates and opens a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
    /// <param name="portName">
    /// <see langword="String" />. Required. Name of the port to open.</param>
    /// <param name="baudRate">
    /// <see langword="Integer" />. Baud rate of the port.</param>
    /// <param name="parity">
    /// <see cref="T:System.IO.Ports.Parity" />. Parity of the port.</param>
    /// <param name="dataBits">
    /// <see langword="Integer" />. Data-bit setting of the port.</param>
    /// <returns>An open <see cref="T:System.IO.Ports.SerialPort" /> object, configured with the supplied arguments.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="portName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="baudRate" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dataBits" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="parity" /> is not one of the <see cref="T:System.IO.Ports.Parity" /> enumeration values.</exception>
    public SerialPort OpenSerialPort(string portName, int baudRate, Parity parity, int dataBits)
    {
      SerialPort serialPort = new SerialPort(portName, baudRate, parity, dataBits);
      serialPort.Open();
      return serialPort;
    }

    /// <summary>Creates and opens a <see cref="T:System.IO.Ports.SerialPort" /> object.</summary>
    /// <param name="portName">
    /// <see langword="String" />. Required. Name of the port to open.</param>
    /// <param name="baudRate">
    /// <see langword="Integer" />. Baud rate of the port.</param>
    /// <param name="parity">
    /// <see cref="T:System.IO.Ports.Parity" />. Parity of the port.</param>
    /// <param name="dataBits">
    /// <see langword="Integer" />. Data-bit setting of the port.</param>
    /// <param name="stopBits">
    /// <see cref="T:System.IO.Ports.StopBits" />. Stop-bit setting of the port.</param>
    /// <returns>An open <see cref="T:System.IO.Ports.SerialPort" /> object, configured with the supplied arguments.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="portName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="baudRate" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dataBits" /> is less than or equal to zero.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="parity" /> is not one of the <see cref="T:System.IO.Ports.Parity" /> enumeration values.</exception>
    /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
    /// <paramref name="stopBits" /> is not one of the <see cref="T:System.IO.Ports.StopBits" /> enumeration values.</exception>
    public SerialPort OpenSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
    {
      SerialPort serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
      serialPort.Open();
      return serialPort;
    }

    /// <summary>Gets a collection of the names of the serial ports on the computer.</summary>
    /// <returns>A collection of the names of the serial ports on the computer.</returns>
    public ReadOnlyCollection<string> SerialPortNames
    {
      get
      {
        string[] portNames = SerialPort.GetPortNames();
        List<string> stringList = new List<string>();
        string[] strArray = portNames;
        int index = 0;
        while (index < strArray.Length)
        {
          string str = strArray[index];
          stringList.Add(str);
          checked { ++index; }
        }
        return new ReadOnlyCollection<string>((IList<string>) stringList);
      }
    }
  }
}
*/