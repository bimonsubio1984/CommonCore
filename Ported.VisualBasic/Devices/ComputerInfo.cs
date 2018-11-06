// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.ComputerInfo
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Ported.VisualBasic.Devices
{
  /// <summary>Provides properties for getting information about the computer's memory, loaded assemblies, name, and operating system.</summary>
  [DebuggerTypeProxy(typeof (ComputerInfo.ComputerInfoDebugView))]
  [//HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class ComputerInfo
  {
    private ManagementBaseObject m_OSManagementObject;
    private ComputerInfo.InternalMemoryStatus m_InternalMemoryStatus;

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Devices.ComputerInfo" /> class. </summary>
    public ComputerInfo()
    {
      this.m_OSManagementObject = (ManagementBaseObject) null;
      this.m_InternalMemoryStatus = (ComputerInfo.InternalMemoryStatus) null;
    }

    /// <summary>Gets the total amount of physical memory for the computer.</summary>
    /// <returns>A <see langword="ULong" /> containing the number of bytes of physical memory for the computer.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The application cannot obtain the memory status.</exception>
    [CLSCompliant(false)]
    public ulong TotalPhysicalMemory
    {
      get
      {
        return this.MemoryStatus.TotalPhysicalMemory;
      }
    }

    /// <summary>Gets the total amount of free physical memory for the computer.</summary>
    /// <returns>A <see langword="ULong" /> containing the number of bytes of free physical memory for the computer.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The application cannot obtain the memory status.</exception>
    [CLSCompliant(false)]
    public ulong AvailablePhysicalMemory
    {
      get
      {
        return this.MemoryStatus.AvailablePhysicalMemory;
      }
    }

    /// <summary>Gets the total amount of virtual address space available for the computer.</summary>
    /// <returns>A <see langword="ULong" /> containing the number of bytes of virtual address space available for the computer.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The application cannot obtain the memory status.</exception>
    [CLSCompliant(false)]
    public ulong TotalVirtualMemory
    {
      get
      {
        return this.MemoryStatus.TotalVirtualMemory;
      }
    }

    /// <summary>Gets the total amount of the computer's free virtual address space.</summary>
    /// <returns>A <see langword="ULong" /> containing the number of bytes of the computer's free virtual address space.</returns>
    /// <exception cref="T:System.ComponentModel.Win32Exception">The application cannot obtain the memory status.</exception>
    [CLSCompliant(false)]
    public ulong AvailableVirtualMemory
    {
      get
      {
        return this.MemoryStatus.AvailableVirtualMemory;
      }
    }

    /// <summary>Gets the current UI culture installed with the operating system.</summary>
    /// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object represents the UI culture installed on the computer.</returns>
    public CultureInfo InstalledUICulture
    {
      get
      {
        return CultureInfo.InstalledUICulture;
      }
    }

    /// <summary>Gets the full operating system name. </summary>
    /// <returns>A <see langword="String" /> containing the operating-system name.</returns>
    /// <exception cref="T:System.Security.SecurityException">The calling code does not have full trust.</exception>
    public string OSFullName
    {
      get
      {
        try
        {
          string index = "Name";
          char ch = '|';
          string str = Conversions.ToString(this.OSManagementBaseObject.Properties[index].Value);
          if (str.Contains(Conversions.ToString(ch)))
            return str.Substring(0, str.IndexOf(ch));
          return str;
        }
        catch (COMException ex)
        {
          return this.OSPlatform;
        }
      }
    }

    /// <summary>Gets the platform identifier for the operating system of the computer.</summary>
    /// <returns>A <see langword="String" /> containing the platform identifier for the operating system of the computer, chosen from the member names of the <see cref="T:System.PlatformID" /> enumeration.</returns>
    /// <exception cref="T:System.ExecutionEngineException">The application cannot obtain the operating-system platform information.</exception>
    public string OSPlatform
    {
      get
      {
        return Environment.OSVersion.Platform.ToString();
      }
    }

    /// <summary>Gets the version of the computer's operating system.</summary>
    /// <returns>A <see langword="String" /> containing the current version number of the operating system.</returns>
    /// <exception cref="T:System.ExecutionEngineException">The application cannot obtain the operating-system version information.</exception>
    public string OSVersion
    {
      get
      {
        return Environment.OSVersion.Version.ToString();
      }
    }

    private ComputerInfo.InternalMemoryStatus MemoryStatus
    {
      get
      {
        if (this.m_InternalMemoryStatus == null)
          this.m_InternalMemoryStatus = new ComputerInfo.InternalMemoryStatus();
        return this.m_InternalMemoryStatus;
      }
    }

    private ManagementBaseObject OSManagementBaseObject
    {
      get
      {
        string queryOrClassName = "Win32_OperatingSystem";
        if (this.m_OSManagementObject == null)
        {
          ManagementObjectCollection objectCollection = new ManagementObjectSearcher((ObjectQuery) new SelectQuery(queryOrClassName)).Get();
          if (objectCollection.Count <= 0)
            throw ExceptionUtils.GetInvalidOperationException("DiagnosticInfo_FullOSName");
          ManagementObjectCollection.ManagementObjectEnumerator enumerator = objectCollection.GetEnumerator();
          enumerator.MoveNext();
          this.m_OSManagementObject = enumerator.Current;
        }
        return this.m_OSManagementObject;
      }
    }

    internal sealed class ComputerInfoDebugView
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      private ComputerInfo m_InstanceBeingWatched;

      public ComputerInfoDebugView(ComputerInfo RealClass)
      {
        this.m_InstanceBeingWatched = RealClass;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public ulong TotalPhysicalMemory
      {
        get
        {
          return this.m_InstanceBeingWatched.TotalPhysicalMemory;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public ulong AvailablePhysicalMemory
      {
        get
        {
          return this.m_InstanceBeingWatched.AvailablePhysicalMemory;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public ulong TotalVirtualMemory
      {
        get
        {
          return this.m_InstanceBeingWatched.TotalVirtualMemory;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public ulong AvailableVirtualMemory
      {
        get
        {
          return this.m_InstanceBeingWatched.AvailableVirtualMemory;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public CultureInfo InstalledUICulture
      {
        get
        {
          return this.m_InstanceBeingWatched.InstalledUICulture;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public string OSPlatform
      {
        get
        {
          return this.m_InstanceBeingWatched.OSPlatform;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public string OSVersion
      {
        get
        {
          return this.m_InstanceBeingWatched.OSVersion;
        }
      }
    }

    private class InternalMemoryStatus
    {
      private bool m_IsOldOS;
      private NativeMethods.MEMORYSTATUS m_MemoryStatus;
      private NativeMethods.MEMORYSTATUSEX m_MemoryStatusEx;

      public InternalMemoryStatus()
      {
        this.m_IsOldOS = Environment.OSVersion.Version.Major < 5;
      }

      internal ulong TotalPhysicalMemory
      {
        get
        {
          this.Refresh();
          if (this.m_IsOldOS)
            return (ulong) this.m_MemoryStatus.dwTotalPhys;
          return this.m_MemoryStatusEx.ullTotalPhys;
        }
      }

      internal ulong AvailablePhysicalMemory
      {
        get
        {
          this.Refresh();
          if (this.m_IsOldOS)
            return (ulong) this.m_MemoryStatus.dwAvailPhys;
          return this.m_MemoryStatusEx.ullAvailPhys;
        }
      }

      internal ulong TotalVirtualMemory
      {
        get
        {
          this.Refresh();
          if (this.m_IsOldOS)
            return (ulong) this.m_MemoryStatus.dwTotalVirtual;
          return this.m_MemoryStatusEx.ullTotalVirtual;
        }
      }

      internal ulong AvailableVirtualMemory
      {
        get
        {
          this.Refresh();
          if (this.m_IsOldOS)
            return (ulong) this.m_MemoryStatus.dwAvailVirtual;
          return this.m_MemoryStatusEx.ullAvailVirtual;
        }
      }

      private void Refresh()
      {
        if (this.m_IsOldOS)
        {
          this.m_MemoryStatus = new NativeMethods.MEMORYSTATUS();
          NativeMethods.GlobalMemoryStatus(ref this.m_MemoryStatus);
        }
        else
        {
          this.m_MemoryStatusEx = new NativeMethods.MEMORYSTATUSEX();
          this.m_MemoryStatusEx.Init();
          if (!NativeMethods.GlobalMemoryStatusEx(ref this.m_MemoryStatusEx))
            throw ExceptionUtils.GetWin32Exception("DiagnosticInfo_Memory");
        }
      }
    }
  }
}

*/