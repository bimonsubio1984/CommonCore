// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.User
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll
/*

using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides access to the information about the current user.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class User
  {
    /// <summary>Gets the name of the current user. </summary>
    /// <returns>
    /// <see langword="String" />. The name of the current user.</returns>
    public string Name
    {
      get
      {
        return this.InternalPrincipal.Identity.Name;
      }
    }

    /// <summary>Gets or sets the current principal (for role-based security).</summary>
    /// <returns>A <see cref="T:System.Security.Principal.IPrincipal" /> value representing the security context.</returns>
    /// <exception cref="T:System.Security.SecurityException">The caller does not have the permission required to set the principal.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IPrincipal CurrentPrincipal
    {
      get
      {
        return this.InternalPrincipal;
      }
      set
      {
        this.InternalPrincipal = value;
      }
    }

    /// <summary>Sets the thread's current principal to the Windows user that started the application.</summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void InitializeWithWindowsUser()
    {
      Thread.CurrentPrincipal = (IPrincipal) new WindowsPrincipal(WindowsIdentity.GetCurrent());
    }

    /// <summary>Gets a value that indicates whether the user has been authenticated.</summary>
    /// <returns>
    /// <see langword="True" /> if the user was authenticated; otherwise, <see langword="False" />.</returns>
    public bool IsAuthenticated
    {
      get
      {
        return this.InternalPrincipal.Identity.IsAuthenticated;
      }
    }

    /// <summary>Determines whether the current user belongs to the specified role. </summary>
    /// <param name="role">The name of the role for which to check membership. </param>
    /// <returns>
    /// <see langword="True" /> if the current user is a member of the specified role; otherwise, <see langword="False" />.</returns>
    public bool IsInRole(string role)
    {
      return this.InternalPrincipal.IsInRole(role);
    }

    /// <summary>Determines whether the current user belongs to the specified role. </summary>
    /// <param name="role">The built-in Windows role for which to check membership. </param>
    /// <returns>
    /// <see langword="True" /> if the current user is a member of the specified role; otherwise, <see langword="False" />.</returns>
    public bool IsInRole(BuiltInRole role)
    {
      User.ValidateBuiltInRoleEnumValue(role, nameof (role));
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (BuiltInRole));
      if (this.IsWindowsPrincipal())
        return ((WindowsPrincipal) this.InternalPrincipal).IsInRole((WindowsBuiltInRole) converter.ConvertTo((object) role, typeof (WindowsBuiltInRole)));
      return this.InternalPrincipal.IsInRole(converter.ConvertToString((object) role));
    }

    /// <summary>Gets or sets the principal object representing the current user.</summary>
    /// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object representing the current user.</returns>
    protected virtual IPrincipal InternalPrincipal
    {
      get
      {
        return Thread.CurrentPrincipal;
      }
      set
      {
        Thread.CurrentPrincipal = value;
      }
    }

    private bool IsWindowsPrincipal()
    {
      return this.InternalPrincipal is WindowsPrincipal;
    }

    internal static void ValidateBuiltInRoleEnumValue(BuiltInRole testMe, string parameterName)
    {
      if (testMe != BuiltInRole.AccountOperator && testMe != BuiltInRole.Administrator && (testMe != BuiltInRole.BackupOperator && testMe != BuiltInRole.Guest) && (testMe != BuiltInRole.PowerUser && testMe != BuiltInRole.PrintOperator && (testMe != BuiltInRole.Replicator && testMe != BuiltInRole.SystemOperator)) && testMe != BuiltInRole.User)
        throw new InvalidEnumArgumentException(parameterName, (int) testMe, typeof (BuiltInRole));
    }
  }
}

*/