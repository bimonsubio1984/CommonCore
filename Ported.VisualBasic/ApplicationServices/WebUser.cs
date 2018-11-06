// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.WebUser
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*
using System.Security.Permissions;
using System.Security.Principal;
using System.Web;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides access to the information about the current user.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class WebUser : User
  {
    /// <summary>Gets or sets the principal object representing the current user.</summary>
    /// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object representing the current user.</returns>
    protected override IPrincipal InternalPrincipal
    {
      get
      {
        return HttpContext.Current.User;
      }
      set
      {
        HttpContext.Current.User = value;
      }
    }
  }
}
*/