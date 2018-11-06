// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.ConsoleApplicationBase
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Deployment.Application;
using System.Security.Permissions;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides properties, methods, and events related to the current application.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class ConsoleApplicationBase : ApplicationBase
  {
    private ReadOnlyCollection<string> m_CommandLineArgs;

    /// <summary>Gets a collection containing the command-line arguments as strings for the current application.</summary>
    /// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see langword="String" />, containing the command-line arguments as strings for the current application.</returns>
    public ReadOnlyCollection<string> CommandLineArgs
    {
      get
      {
        if (this.m_CommandLineArgs == null)
        {
          string[] commandLineArgs = Environment.GetCommandLineArgs();
          if (commandLineArgs.GetLength(0) >= 2)
          {
            string[] strArray = new string[checked (commandLineArgs.GetLength(0) - 2 + 1)];
            Array.Copy((Array) commandLineArgs, 1, (Array) strArray, 0, checked (commandLineArgs.GetLength(0) - 1));
            this.m_CommandLineArgs = new ReadOnlyCollection<string>((IList<string>) strArray);
          }
          else
            this.m_CommandLineArgs = new ReadOnlyCollection<string>((IList<string>) new string[0]);
        }
        return this.m_CommandLineArgs;
      }
    }

    /// <summary>Gets the current application's ClickOnce deployment object, which provides support for updating the current deployment programmatically and support for the on-demand download of files.</summary>
    /// <returns>Returns the <see cref="T:System.Deployment.Application.ApplicationDeployment" /> object for the application's ClickOnce deployment.</returns>
    /// <exception cref="T:System.Deployment.Application.InvalidDeploymentException">The application is not deployed as a ClickOnce application.</exception>
    public ApplicationDeployment Deployment
    {
      get
      {
        return ApplicationDeployment.CurrentDeployment;
      }
    }

    /// <summary>Gets a <see langword="Boolean" /> that represents whether the application was deployed from a network using ClickOnce.</summary>
    /// <returns>A <see langword="Boolean" /> that represents whether the application was deployed from a network. The value is <see langword="True" /> if the current application was deployed from a network; otherwise the value is <see langword="False" />.</returns>
    public bool IsNetworkDeployed
    {
      get
      {
        return ApplicationDeployment.IsNetworkDeployed;
      }
    }

    /// <summary>Sets the values to use as the current application's command-line arguments.</summary>
    /// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see langword="String" />, containing the strings to use as the command-line arguments for the current application.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected ReadOnlyCollection<string> InternalCommandLine
    {
      set
      {
        this.m_CommandLineArgs = value;
      }
    }
  }
}

*/