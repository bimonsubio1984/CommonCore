// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.ApplicationBase
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll
/*

using Ported.VisualBasic.CompilerServices;
using Ported.VisualBasic.Logging;
using System;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides properties, methods, and events related to the current application.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class ApplicationBase
  {
    private Log m_Log;
    private AssemblyInfo m_Info;

    /// <summary>Returns the value of the specified environment variable.</summary>
    /// <param name="name">A <see langword="String" /> containing the name of the environment variable.</param>
    /// <returns>A <see langword="String" /> containing the value of the environment variable with the name <paramref name="name" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="name" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">The environment variable specified by <paramref name="name" /> does not exist.</exception>
    /// <exception cref="T:System.Security.SecurityException">The calling code does not have <see cref="T:System.Security.Permissions.EnvironmentPermission" /> with <see langword="Read" /> access.</exception>
    public string GetEnvironmentVariable(string name)
    {
      string environmentVariable = Environment.GetEnvironmentVariable(name);
      if (environmentVariable == null)
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (name), "EnvVarNotFound_Name", name);
      return environmentVariable;
    }

    /// <summary>Gets an object that provides properties and methods for writing event and exception information to the application's log listeners. </summary>
    /// <returns>The <see cref="T:Ported.VisualBasic.Logging.Log" /> object for the current application.</returns>
    public Log Log
    {
      get
      {
        if (this.m_Log == null)
          this.m_Log = new Log();
        return this.m_Log;
      }
    }

    /// <summary>Gets an object that provides properties for getting information about the application's assembly, such as the version number, description, and so on. </summary>
    /// <returns>The <see cref="T:Ported.VisualBasic.ApplicationServices.AssemblyInfo" /> object for the current application.</returns>
    public AssemblyInfo Info
    {
      [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)] get
      {
        if (this.m_Info == null)
          this.m_Info = new AssemblyInfo(Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly());
        return this.m_Info;
      }
    }

    /// <summary>Gets the culture that the current thread uses for string manipulation and string formatting.</summary>
    /// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture the current thread uses for string manipulation and string formatting.</returns>
    public CultureInfo Culture
    {
      get
      {
        return Thread.CurrentThread.CurrentCulture;
      }
    }

    /// <summary>Gets the culture that the current thread uses for retrieving culture-specific resources.</summary>
    /// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture that the current thread uses for retrieving culture-specific resources.</returns>
    public CultureInfo UICulture
    {
      get
      {
        return Thread.CurrentThread.CurrentUICulture;
      }
    }

    /// <summary>Changes the culture used by the current thread for string manipulation and for string formatting.</summary>
    /// <param name="cultureName">
    /// <see langword="String" />. Name of the culture as a string. For a list of possible names, see <see cref="T:System.Globalization.CultureInfo" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cultureName" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="cultureName" /> is not a valid culture name.</exception>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public void ChangeCulture(string cultureName)
    {
      Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
    }

    /// <summary>Changes the culture that the current thread uses for retrieving culture-specific resources.</summary>
    /// <param name="cultureName">
    /// <see langword="String" />. Name of the culture as a string. For a list of possible names, see <see cref="T:System.Globalization.CultureInfo" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="cultureName" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="cultureName" /> is not a valid culture name.</exception>
    public void ChangeUICulture(string cultureName)
    {
      Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
    }
  }
}

*/