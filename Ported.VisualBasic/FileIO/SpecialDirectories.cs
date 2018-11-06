// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.SpecialDirectories
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.CompilerServices;
using System;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Provides properties for accessing commonly referenced directories.</summary>
  [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class SpecialDirectories
  {
    /// <summary>Gets a path name pointing to the My Documents directory.</summary>
    /// <returns>The path to the My Documents directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string MyDocuments
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IO_SpecialDirectory_MyDocuments");
      }
    }

    /// <summary>Gets a path name pointing to the My Music directory.</summary>
    /// <returns>The path to the My Music directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string MyMusic
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "IO_SpecialDirectory_MyMusic");
      }
    }

    /// <summary>Gets a path name pointing to the My Pictures directory.</summary>
    /// <returns>The path to the My Pictures directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string MyPictures
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "IO_SpecialDirectory_MyPictures");
      }
    }

    /// <summary>Gets a path name pointing to the Desktop directory.</summary>
    /// <returns>The path to the Desktop directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string Desktop
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "IO_SpecialDirectory_Desktop");
      }
    }

    /// <summary>Gets a path name pointing to the Programs directory.</summary>
    /// <returns>The path to the Programs directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string Programs
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "IO_SpecialDirectory_Programs");
      }
    }

    /// <summary>Gets a path pointing to the Program Files directory.</summary>
    /// <returns>The path to the Program Files directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string ProgramFiles
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "IO_SpecialDirectory_ProgramFiles");
      }
    }

    /// <summary>Gets a path name pointing to the Temp directory.</summary>
    /// <returns>The path to the Temp directory.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string Temp
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Path.GetTempPath(), "IO_SpecialDirectory_Temp");
      }
    }

    /// <summary>Gets a path name pointing to the Application Data directory for the current user.</summary>
    /// <returns>The path to the Application Data directory for the current user.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string CurrentUserApplicationData
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Application.UserAppDataPath, "IO_SpecialDirectory_UserAppData");
      }
    }

    /// <summary>Gets a path name pointing to the Application Data directory for the all users.</summary>
    /// <returns>The path to the Application Data directory for the all users.</returns>
    /// <exception cref="T:System.Security.Permissions.EnvironmentPermission">Controls access to system and user environment variables. Associated enumeration: <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is empty, usually because the operating system does not support the directory.</exception>
    public static string AllUsersApplicationData
    {
      get
      {
        return SpecialDirectories.GetDirectoryPath(Application.CommonAppDataPath, "IO_SpecialDirectory_AllUserAppData");
      }
    }

    private static string GetDirectoryPath(string Directory, string DirectoryNameResID)
    {
      if (Operators.CompareString(Directory, "", false) == 0)
        throw ExceptionUtils.GetDirectoryNotFoundException("IO_SpecialDirectoryNotExist", Utils.GetResourceString(DirectoryNameResID));
      return FileSystem.NormalizePath(Directory);
    }
  }
}

*/