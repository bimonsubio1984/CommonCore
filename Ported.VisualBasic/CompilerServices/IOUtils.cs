// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.IOUtils
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class IOUtils
  {
    private IOUtils()
    {
    }

    internal static string FindFirstFile(Assembly assem, string PathName, FileAttributes Attributes)
    {
      string str1 = (string) null;
      string str2;
      if (PathName.Length > 0 && (int) PathName[checked (PathName.Length - 1)] == (int) Path.DirectorySeparatorChar)
      {
        str1 = Path.GetFullPath(PathName);
        str2 = "*.*";
      }
      else
      {
        if (PathName.Length == 0)
        {
          str2 = "*.*";
        }
        else
        {
          str2 = Path.GetFileName(PathName);
          str1 = Path.GetDirectoryName(PathName);
          if (str2 == null || str2.Length == 0 || Operators.CompareString(str2, ".", false) == 0)
            str2 = "*.*";
        }
        if (str1 == null || str1.Length == 0)
        {
          if (Path.IsPathRooted(PathName))
          {
            str1 = Path.GetPathRoot(PathName);
          }
          else
          {
            str1 = Environment.CurrentDirectory;
            if ((int) str1[checked (str1.Length - 1)] != (int) Path.DirectorySeparatorChar)
              str1 += Conversions.ToString(Path.DirectorySeparatorChar);
          }
        }
        else if ((int) str1[checked (str1.Length - 1)] != (int) Path.DirectorySeparatorChar)
          str1 += Conversions.ToString(Path.DirectorySeparatorChar);
        if (Operators.CompareString(str2, "..", false) == 0)
        {
          str1 += "..\\";
          str2 = "*.*";
        }
      }
      FileSystemInfo[] fileSystemInfos;
      try
      {
        fileSystemInfos = Directory.GetParent(str1 + str2).GetFileSystemInfos(str2);
      }
      catch (SecurityException ex)
      {
        throw ex;
      }
      catch (IOException ex) when (Marshal.GetHRForException((Exception) ex) == -2147024875)
      {
        throw ExceptionUtils.VbMakeException(52);
      }
      catch (StackOverflowException ex)
      {
        throw ex;
      }
      catch (OutOfMemoryException ex)
      {
        throw ex;
      }
      catch (ThreadAbortException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        return "";
      }
      AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(assem);
      assemblyData.m_DirFiles = fileSystemInfos;
      assemblyData.m_DirNextFileIndex = 0;
      assemblyData.m_DirAttributes = Attributes;
      if (fileSystemInfos == null || fileSystemInfos.Length == 0)
        return "";
      return IOUtils.FindFileFilter(assemblyData);
    }

    internal static string FindNextFile(Assembly assem)
    {
      AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(assem);
      if (assemblyData.m_DirFiles == null)
        throw new ArgumentException(Utils.GetResourceString("DIR_IllegalCall"));
      if (assemblyData.m_DirNextFileIndex <= assemblyData.m_DirFiles.GetUpperBound(0))
        return IOUtils.FindFileFilter(assemblyData);
      assemblyData.m_DirFiles = (FileSystemInfo[]) null;
      assemblyData.m_DirNextFileIndex = 0;
      return (string) null;
    }

    private static string FindFileFilter(AssemblyData oAssemblyData)
    {
      FileSystemInfo[] dirFiles = oAssemblyData.m_DirFiles;
      int dirNextFileIndex = oAssemblyData.m_DirNextFileIndex;
      while (dirNextFileIndex <= dirFiles.GetUpperBound(0))
      {
        FileSystemInfo fileSystemInfo = dirFiles[dirNextFileIndex];
        if ((fileSystemInfo.Attributes & (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Directory)) == (FileAttributes) 0 || (fileSystemInfo.Attributes & oAssemblyData.m_DirAttributes) != (FileAttributes) 0)
        {
          oAssemblyData.m_DirNextFileIndex = checked (dirNextFileIndex + 1);
          return dirFiles[dirNextFileIndex].Name;
        }
        checked { ++dirNextFileIndex; }
      }
      oAssemblyData.m_DirFiles = (FileSystemInfo[]) null;
      oAssemblyData.m_DirNextFileIndex = 0;
      return (string) null;
    }
  }
}

*/