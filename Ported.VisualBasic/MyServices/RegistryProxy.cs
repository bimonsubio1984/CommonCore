// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyServices.RegistryProxy
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Microsoft.Win32;
using System.ComponentModel;
using System.Security.Permissions;

namespace Ported.VisualBasic.MyServices
{
  /// <summary>Provides properties and methods for manipulating the registry.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class RegistryProxy
  {
    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type which provides access to <see langword="HKEY_CURRENT_USER" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey CurrentUser
    {
      get
      {
        return Registry.CurrentUser;
      }
    }

    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type, which provides access to <see langword="HKEY_LOCAL_MACHINE" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey LocalMachine
    {
      get
      {
        return Registry.LocalMachine;
      }
    }

    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type which provides access to <see langword="HKEY_CLASSES_ROOT" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey ClassesRoot
    {
      get
      {
        return Registry.ClassesRoot;
      }
    }

    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type, which provides access to <see langword="HKEY_USERS" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey Users
    {
      get
      {
        return Registry.Users;
      }
    }

    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type, which provides access to <see langword="HKEY_PERFORMANCE_DATA" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey PerformanceData
    {
      get
      {
        return Registry.PerformanceData;
      }
    }

    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type which provides access to <see langword="HKEY_CURRENT_CONFIG" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey CurrentConfig
    {
      get
      {
        return Registry.CurrentConfig;
      }
    }

    /// <summary>Returns a <see cref="T:Microsoft.Win32.RegistryKey" /> type, which provides access to <see langword="HKEY_DYNDATA" />.</summary>
    /// <returns>
    ///   <see cref="T:Microsoft.Win32.RegistryKey" />
    /// </returns>
    public RegistryKey DynData
    {
      get
      {
        return Registry.DynData;
      }
    }

    /// <summary>Gets a value from a registry key.</summary>
    /// <param name="keyName">
    /// <see langword="String" />. Key from which the value is to be retrieved. Required. </param>
    /// <param name="valueName">
    /// <see langword="String" />. Value to be retrieved. Required. </param>
    /// <param name="defaultValue">
    /// <see langword="Object" />. Default value to be supplied if the value does not exist. Required. </param>
    /// <returns>Gets a value from a registry key.</returns>
    /// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
    /// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="keyName" /> does not begin with a valid registry root. </exception>
    public object GetValue(string keyName, string valueName, object defaultValue)
    {
      return Registry.GetValue(keyName, valueName, defaultValue);
    }

    /// <summary>Writes a value to a registry key.</summary>
    /// <param name="keyName">
    /// <see langword="String" />. Name of the key to be written to. Required. </param>
    /// <param name="valueName">
    /// <see langword="String" />. Name of the value to be written. Required. </param>
    /// <param name="value">
    /// <see langword="Object" />. Value to be written. Required. </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is <see langword="Nothing" />. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="keyName" /> does not begin with a valid registry root.-or-
    /// <paramref name="valueName" /> is longer than the maximum length allowed (255 characters). </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only and thus cannot be written to; for example, it is a root-level node, or it has not been opened with write access.. </exception>
    /// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
    public void SetValue(string keyName, string valueName, object value)
    {
      Registry.SetValue(keyName, valueName, value);
    }

    /// <summary>Writes a value to a registry key.</summary>
    /// <param name="keyName">
    /// <see langword="String" />. Name of the key to be written to. Required. </param>
    /// <param name="valueName">
    /// <see langword="String" />. Name of the value to be written. Required. </param>
    /// <param name="value">
    /// <see langword="Object" />. Value to be written. Required. </param>
    /// <param name="valueKind">
    /// <see cref="T:Microsoft.Win32.RegistryValueKind" />. Required. </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is <see langword="Nothing" />. </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="keyName" /> does not begin with a valid registry root.-or-
    /// <paramref name="keyName" /> is longer than the maximum length allowed (255 characters).-or- The type of <paramref name="value" /> does not match the registry data type specified by <paramref name="valueKind" />, therefore the data cannot be converted properly. </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and thus cannot be written to; for example, it is a root-level node, or it has not been opened with write access. </exception>
    /// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
    public void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind)
    {
      Registry.SetValue(keyName, valueName, value, valueKind);
    }

    internal RegistryProxy()
    {
    }
  }
}

*/