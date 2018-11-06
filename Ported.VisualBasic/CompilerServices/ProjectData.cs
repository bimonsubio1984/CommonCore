// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ProjectData
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll



using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>Provides helpers for the Visual Basic <see langword="Err" /> object. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class ProjectData
  {
    internal ErrObject m_Err;
    internal int m_rndSeed;
    internal byte[] m_numprsPtr;
    internal byte[] m_DigitArray;
    internal Hashtable m_AssemblyData;
    [ThreadStatic]
    private static ProjectData m_oProject;
    private Assembly m_CachedMSCoreLibAssembly;

    private ProjectData()
    {
      this.m_rndSeed = 327680;
      this.m_CachedMSCoreLibAssembly = typeof (int).Assembly;
      this.m_AssemblyData = new Hashtable();
      this.m_numprsPtr = new byte[24];
      this.m_DigitArray = new byte[30];
    }

    internal AssemblyData GetAssemblyData(Assembly assem)
    {
      if (assem == Utils.VBRuntimeAssembly || assem == this.m_CachedMSCoreLibAssembly)
        throw new SecurityException(Utils.GetResourceString("Security_LateBoundCallsNotPermitted"));
      AssemblyData assemblyData = (AssemblyData) this.m_AssemblyData[(object) assem];
      if (assemblyData == null)
      {
        assemblyData = new AssemblyData();
        this.m_AssemblyData[(object) assem] = (object) assemblyData;
      }
      return assemblyData;
    }

    internal static ProjectData GetProjectData()
    {
      ProjectData projectData = ProjectData.m_oProject;
      if (projectData == null)
      {
        projectData = new ProjectData();
        ProjectData.m_oProject = projectData;
      }
      return projectData;
    }

    /// <summary>Performs the work for the <see langword="Raise" /> method of the <see langword="Err" /> object. A helper method.</summary>
    /// <param name="hr">An integer value that identifies the nature of the error. Visual Basic errors are in the range 0–65535; the range 0–512 is reserved for system errors; the range 513–65535 is available for user-defined errors.</param>
    /// <returns>An <see cref="T:System.Exception" /> object.</returns>
    public static Exception CreateProjectError(int hr)
    {
      ErrObject errObject = Information.Err();
      errObject.Clear();
      int num = errObject.MapErrorNumber(hr);
      return errObject.CreateException(hr, Utils.GetResourceString((vbErrors) num));
    }

    /// <summary>The Visual Basic compiler uses this helper method to capture exceptions in the <see langword="Err" /> object.</summary>
    /// <param name="ex">The <see cref="T:System.Exception" /> object to be caught.</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void SetProjectError(Exception ex)
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        Information.Err().CaptureException(ex);
      }
    }

    /// <summary>The Visual Basic compiler uses this helper method to capture exceptions in the <see langword="Err" /> object.</summary>
    /// <param name="ex">The <see cref="T:System.Exception" /> object to be caught.</param>
    /// <param name="lErl">The line number of the exception.</param>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void SetProjectError(Exception ex, int lErl)
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        Information.Err().CaptureException(ex, lErl);
      }
    }

    /// <summary>Performs the work for the <see langword="Clear" /> method of the <see langword="Err" /> object. A helper method.</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void ClearProjectError()
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        Information.Err().Clear();
      }
    }

    /*
    /// <summary>Closes all files for the calling assembly and stops the process.</summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.SelfAffectingProcessMgmt)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static void EndApp()
    {
      FileSystem.CloseAllFiles(Assembly.GetCallingAssembly());
      Environment.Exit(0);
    }
    */
  }
}

