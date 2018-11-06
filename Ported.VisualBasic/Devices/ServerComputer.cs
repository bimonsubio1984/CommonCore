// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.ServerComputer
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*
using Ported.VisualBasic.MyServices;
using System;
using System.Security.Permissions;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides properties for manipulating computer components such as audio, the clock, the keyboard, the file system, and so on.</summary>
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class ServerComputer
  {
    private ComputerInfo m_ComputerInfo;
    private FileSystemProxy m_FileIO;
    private Network m_Network;
    private RegistryProxy m_RegistryInstance;
    private static Clock m_Clock;

    /// <summary>Gets an object that provides properties for accessing the current local time and Universal Coordinated Time (the equivalent to Greenwich Mean Time) from the system clock.</summary>
    /// <returns>The <see langword="My.Computer.Clock" /> object for the computer.</returns>
    public Clock Clock
    {
      get
      {
        if (ServerComputer.m_Clock != null)
          return ServerComputer.m_Clock;
        ServerComputer.m_Clock = new Clock();
        return ServerComputer.m_Clock;
      }
    }

    /// <summary>Gets an object that provides properties and methods for working with drives, files, and directories.</summary>
    /// <returns>The <see langword="My.Computer.FileSystem" /> object for the computer.</returns>
    public FileSystemProxy FileSystem
    {
      get
      {
        if (this.m_FileIO == null)
          this.m_FileIO = new FileSystemProxy();
        return this.m_FileIO;
      }
    }

    /// <summary>Gets an object that provides properties for getting information about the computer's memory, loaded assemblies, name, and operating system.</summary>
    /// <returns>The <see langword="My.Computer.Info" /> object for the computer.</returns>
    public ComputerInfo Info
    {
      get
      {
        if (this.m_ComputerInfo == null)
          this.m_ComputerInfo = new ComputerInfo();
        return this.m_ComputerInfo;
      }
    }

    /// <summary>Gets an object that provides a property and methods for interacting with the network to which the computer is connected.</summary>
    /// <returns>The <see langword="My.Computer.Network" /> object for the computer.</returns>
    public Network Network
    {
      get
      {
        if (this.m_Network != null)
          return this.m_Network;
        this.m_Network = new Network();
        return this.m_Network;
      }
    }

    /// <summary>Gets the computer name.</summary>
    /// <returns>A <see langword="String" /> containing the name of the computer.</returns>
    public string Name
    {
      get
      {
        return Environment.MachineName;
      }
    }

    /// <summary>Gets an object that provides properties and methods for manipulating the registry.</summary>
    /// <returns>The <see langword="My.Computer.Registry" /> object for the computer.</returns>
    public RegistryProxy Registry
    {
      get
      {
        if (this.m_RegistryInstance != null)
          return this.m_RegistryInstance;
        this.m_RegistryInstance = new RegistryProxy();
        return this.m_RegistryInstance;
      }
    }
  }
}
*/