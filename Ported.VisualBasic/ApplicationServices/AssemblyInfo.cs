// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.AssemblyInfo
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll
/*

using Ported.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides properties for getting the information about the application, such as the version number, description, loaded assemblies, and so on.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class AssemblyInfo
  {
    private Assembly m_Assembly;
    private string m_Description;
    private string m_Title;
    private string m_ProductName;
    private string m_CompanyName;
    private string m_Trademark;
    private string m_Copyright;

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.AssemblyInfo" /> class with the specified assembly information. </summary>
    /// <param name="currentAssembly">
    /// <see cref="T:System.Reflection.Assembly" />. The assembly for which to obtain the information.</param>
    public AssemblyInfo(Assembly currentAssembly)
    {
      this.m_Description = (string) null;
      this.m_Title = (string) null;
      this.m_ProductName = (string) null;
      this.m_CompanyName = (string) null;
      this.m_Trademark = (string) null;
      this.m_Copyright = (string) null;
      if (currentAssembly == null)
        throw ExceptionUtils.GetArgumentNullException("CurrentAssembly");
      this.m_Assembly = currentAssembly;
    }

    /// <summary>Gets the description associated with the application.</summary>
    /// <returns>A <see langword="String" /> containing the description associated with the application.</returns>
    /// <exception cref="T:System.InvalidOperationException">The assembly does not have an <see cref="T:System.Reflection.AssemblyDescriptionAttribute" /> attribute.</exception>
    public string Description
    {
      get
      {
        if (this.m_Description == null)
        {
          AssemblyDescriptionAttribute attribute = (AssemblyDescriptionAttribute) this.GetAttribute(typeof (AssemblyDescriptionAttribute));
          this.m_Description = attribute != null ? attribute.Description : "";
        }
        return this.m_Description;
      }
    }

    /// <summary>Gets the company name associated with the application.</summary>
    /// <returns>A <see langword="String" /> that contains the company name associated with the application.</returns>
    /// <exception cref="T:System.InvalidOperationException">The assembly does not have an <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> attribute.</exception>
    public string CompanyName
    {
      get
      {
        if (this.m_CompanyName == null)
        {
          AssemblyCompanyAttribute attribute = (AssemblyCompanyAttribute) this.GetAttribute(typeof (AssemblyCompanyAttribute));
          this.m_CompanyName = attribute != null ? attribute.Company : "";
        }
        return this.m_CompanyName;
      }
    }

    /// <summary>Gets the title associated with the application.</summary>
    /// <returns>A <see langword="String" /> containing the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> associated with the application.</returns>
    /// <exception cref="T:System.InvalidOperationException">The assembly does not have an <see cref="T:System.Reflection.AssemblyTitleAttribute" /> attribute.</exception>
    public string Title
    {
      get
      {
        if (this.m_Title == null)
        {
          AssemblyTitleAttribute attribute = (AssemblyTitleAttribute) this.GetAttribute(typeof (AssemblyTitleAttribute));
          this.m_Title = attribute != null ? attribute.Title : "";
        }
        return this.m_Title;
      }
    }

    /// <summary>Gets the copyright notice associated with the application.</summary>
    /// <returns>A <see langword="String" /> containing the copyright notice associated with the application.</returns>
    /// <exception cref="T:System.InvalidOperationException">The assembly does not have an <see cref="T:System.Reflection.AssemblyCopyrightAttribute" /> attribute.</exception>
    public string Copyright
    {
      get
      {
        if (this.m_Copyright == null)
        {
          AssemblyCopyrightAttribute attribute = (AssemblyCopyrightAttribute) this.GetAttribute(typeof (AssemblyCopyrightAttribute));
          this.m_Copyright = attribute != null ? attribute.Copyright : "";
        }
        return this.m_Copyright;
      }
    }

    /// <summary>Gets the trademark notice associated with the application.</summary>
    /// <returns>A <see langword="String" /> containing the trademark notice associated with the application.</returns>
    /// <exception cref="T:System.InvalidOperationException">The assembly does not have an <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> attribute.</exception>
    public string Trademark
    {
      get
      {
        if (this.m_Trademark == null)
        {
          AssemblyTrademarkAttribute attribute = (AssemblyTrademarkAttribute) this.GetAttribute(typeof (AssemblyTrademarkAttribute));
          this.m_Trademark = attribute != null ? attribute.Trademark : "";
        }
        return this.m_Trademark;
      }
    }

    /// <summary>Gets the product name associated with the application.</summary>
    /// <returns>A <see langword="String" /> containing the product name associated with the application.</returns>
    /// <exception cref="T:System.InvalidOperationException">The assembly does not have an <see cref="T:System.Reflection.AssemblyProductAttribute" /> attribute.</exception>
    public string ProductName
    {
      get
      {
        if (this.m_ProductName == null)
        {
          AssemblyProductAttribute attribute = (AssemblyProductAttribute) this.GetAttribute(typeof (AssemblyProductAttribute));
          this.m_ProductName = attribute != null ? attribute.Product : "";
        }
        return this.m_ProductName;
      }
    }

    /// <summary>Gets the version number of the application.</summary>
    /// <returns>A <see cref="T:System.Version" /> object containing the version number of the application.</returns>
    /// <exception cref="T:System.Security.SecurityException">The application does not have sufficient permissions to access the assembly version.</exception>
    public Version Version
    {
      get
      {
        return this.m_Assembly.GetName().Version;
      }
    }

    /// <summary>Gets the name, without the extension, of the assembly file for the application.</summary>
    /// <returns>A <see langword="String" /> containing the file name.</returns>
    public string AssemblyName
    {
      get
      {
        return this.m_Assembly.GetName().Name;
      }
    }

    /// <summary>Gets the directory where the application is stored.</summary>
    /// <returns>A <see langword="String" /> that contains the directory where the application is stored.</returns>
    public string DirectoryPath
    {
      get
      {
        return Path.GetDirectoryName(this.m_Assembly.Location);
      }
    }

    /// <summary>Gets a collection of all assemblies loaded by the application.</summary>
    /// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Reflection.Assembly" /> containing all the assemblies loaded by the application.</returns>
    /// <exception cref="T:System.AppDomainUnloadedException">The application domain is not loaded.</exception>
    public ReadOnlyCollection<Assembly> LoadedAssemblies
    {
      get
      {
        Collection<Assembly> collection = new Collection<Assembly>();
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        int index = 0;
        while (index < assemblies.Length)
        {
          Assembly assembly = assemblies[index];
          collection.Add(assembly);
          checked { ++index; }
        }
        return new ReadOnlyCollection<Assembly>((IList<Assembly>) collection);
      }
    }

    /// <summary>Gets the current stack-trace information.</summary>
    /// <returns>A <see langword="String" /> containing the current stack-trace information. The return value can be <see cref="F:System.String.Empty" />.</returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The requested stack-trace information is out of range.</exception>
    public string StackTrace
    {
      get
      {
        return Environment.StackTrace;
      }
    }

    /// <summary>Gets the amount of physical memory mapped to the process context.</summary>
    /// <returns>A <see langword="Long" /> containing the number of bytes of physical memory mapped to the process context.</returns>
    /// <exception cref="T:System.Security.SecurityException">A situation in which partial trust exists and the user lacks necessary permissions.</exception>
    public long WorkingSet
    {
      get
      {
        return Environment.WorkingSet;
      }
    }

    private object GetAttribute(Type AttributeType)
    {
      object[] customAttributes = this.m_Assembly.GetCustomAttributes(AttributeType, true);
      if (customAttributes.Length == 0)
        return (object) null;
      return customAttributes[0];
    }
  }
}
*/