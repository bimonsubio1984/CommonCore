// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileSystem
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll
/*
using Ported.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="FileSystem" /> module contains the procedures that are used to perform file, directory or folder, and system operations. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than using the <see langword="FileSystem" /> module. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
  [StandardModule]
  [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public sealed class FileSystem
  {
    internal static readonly DateTimeFormatInfo m_WriteDateFormatInfo = FileSystem.InitializeWriteDateFormatInfo();
    private const int ERROR_ACCESS_DENIED = 5;
    private const int ERROR_FILE_NOT_FOUND = 2;
    private const int ERROR_BAD_NETPATH = 53;
    private const int ERROR_INVALID_PARAMETER = 87;
    private const int ERROR_WRITE_PROTECT = 19;
    private const int ERROR_FILE_EXISTS = 80;
    private const int ERROR_ALREADY_EXISTS = 183;
    private const int ERROR_INVALID_ACCESS = 12;
    private const int ERROR_NOT_SAME_DEVICE = 17;
    internal const int FIRST_LOCAL_CHANNEL = 1;
    internal const int LAST_LOCAL_CHANNEL = 255;
    private const int A_NORMAL = 0;
    private const int A_RDONLY = 1;
    private const int A_HIDDEN = 2;
    private const int A_SYSTEM = 4;
    private const int A_VOLID = 8;
    private const int A_SUBDIR = 16;
    private const int A_ARCH = 32;
    private const int A_ALLBITS = 63;
    internal const string sTimeFormat = "T";
    internal const string sDateFormat = "d";
    internal const string sDateTimeFormat = "F";

    private static DateTimeFormatInfo InitializeWriteDateFormatInfo()
    {
      return new DateTimeFormatInfo()
      {
        DateSeparator = "-",
        ShortDatePattern = "\\#yyyy-MM-dd\\#",
        LongTimePattern = "\\#HH:mm:ss\\#",
        FullDateTimePattern = "\\#yyyy-MM-dd HH:mm:ss\\#"
      };
    }

    /// <summary>Changes the current directory or folder. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than the <see langword="ChDir " />function. For more information, see <see cref="P:Ported.VisualBasic.FileIO.FileSystem.CurrentDirectory" /> .</summary>
    /// <param name="Path">Required. A <see langword="String" /> expression that identifies which directory or folder becomes the new default directory or folder. <paramref name="Path" /> may include the drive. If no drive is specified, <see langword="ChDir" /> changes the default directory or folder on the current drive. </param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Path" /> is empty.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">Invalid drive is specified, or drive is unavailable.</exception>
    public static void ChDir(string Path)
    {
      Path = Strings.RTrim(Path);
      if (Path == null || Path.Length == 0)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty")), 52);
      if (Operators.CompareString(Path, "\\", false) == 0)
        Path = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
      try
      {
        Directory.SetCurrentDirectory(Path);
      }
      catch (FileNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) new FileNotFoundException(Utils.GetResourceString("FileSystem_PathNotFound1", new string[1]
        {
          Path
        })), 76);
      }
    }

    /// <summary>Changes the current drive.</summary>
    /// <param name="Drive">Required. String expression that specifies an existing drive. If you supply a zero-length string (""), the current drive does not change. If the <paramref name="Drive" /> argument is a multiple-character string, <see langword="ChDrive" /> uses only the first letter.</param>
    /// <exception cref="T:System.IO.IOException">Invalid drive is specified, or drive is unavailable.</exception>
    public static void ChDrive(char Drive)
    {
      Drive = char.ToUpper(Drive, CultureInfo.InvariantCulture);
      if (Drive < 'A' || Drive > 'Z')
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Drive)
        }));
      if (!FileSystem.UnsafeValidDrive(Drive))
        throw ExceptionUtils.VbMakeException((Exception) new IOException(Utils.GetResourceString("FileSystem_DriveNotFound1", new string[1]
        {
          Conversions.ToString(Drive)
        })), 68);
      Directory.SetCurrentDirectory(Conversions.ToString(Drive) + Conversions.ToString(Path.VolumeSeparatorChar));
    }

    /// <summary>Changes the current drive.</summary>
    /// <param name="Drive">Required. String expression that specifies an existing drive. If you supply a zero-length string (""), the current drive does not change. If the <paramref name="Drive" /> argument is a multiple-character string, <see langword="ChDrive" /> uses only the first letter.</param>
    /// <exception cref="T:System.IO.IOException">Invalid drive is specified, or drive is unavailable.</exception>
    public static void ChDrive(string Drive)
    {
      if (Drive == null || Drive.Length == 0)
        return;
      FileSystem.ChDrive(Drive[0]);
    }

    /// <summary>Returns a string representing the current path. The <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />  gives you better productivity and performance in file I/O operations than <see langword="CurDir" />. For more information, see <see cref="P:Ported.VisualBasic.FileIO.FileSystem.CurrentDirectory" />.</summary>
    /// <returns>A string representing the current path.</returns>
    public static string CurDir()
    {
      return Directory.GetCurrentDirectory();
    }

    /// <summary>Returns a string representing the current path. The <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />  gives you better productivity and performance in file I/O operations than <see langword="CurDir" />. For more information, see <see cref="P:Ported.VisualBasic.FileIO.FileSystem.CurrentDirectory" />.</summary>
    /// <param name="Drive">Optional. <see langword="Char" /> expression that specifies an existing drive. If no drive is specified, or if <paramref name="Drive" /> is a zero-length string (""), <see langword="CurDir" /> returns the path for the current drive.</param>
    /// <returns>A string representing the current path. </returns>
    public static string CurDir(char Drive)
    {
      Drive = char.ToUpper(Drive, CultureInfo.InvariantCulture);
      if (Drive < 'A' || Drive > 'Z')
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Drive)
        })), 68);
      string fullPath = Path.GetFullPath(Conversions.ToString(Drive) + Conversions.ToString(Path.VolumeSeparatorChar) + ".");
      if (!FileSystem.UnsafeValidDrive(Drive))
        throw ExceptionUtils.VbMakeException((Exception) new IOException(Utils.GetResourceString("FileSystem_DriveNotFound1", new string[1]
        {
          Conversions.ToString(Drive)
        })), 68);
      return fullPath;
    }

    /// <summary>Returns a string representing the name of a file, directory, or folder that matches a specified pattern or file attribute, or the volume label of a drive. The <see cref="T:Ported.VisualBasic.FileIO.FileSystem" /> gives you better productivity and performance in file I/O operations than the <see langword="Dir" /> function. See <see cref="M:Ported.VisualBasic.FileIO.FileSystem.GetDirectoryInfo(System.String)" />  for more information.</summary>
    /// <returns>A string representing the name of a file, directory, or folder that matches a specified pattern or file attribute, or the volume label of a drive.</returns>
    public static string Dir()
    {
      return IOUtils.FindNextFile(Assembly.GetCallingAssembly());
    }

    /// <summary>Returns a string representing the name of a file, directory, or folder that matches a specified pattern or file attribute, or the volume label of a drive. The <see cref="T:Ported.VisualBasic.FileIO.FileSystem" /> gives you better productivity and performance in file I/O operations than the <see langword="Dir" /> function. See <see cref="M:Ported.VisualBasic.FileIO.FileSystem.GetDirectoryInfo(System.String)" />  for more information.</summary>
    /// <param name="PathName">Optional. <see langword="String" /> expression that specifies a file name, directory or folder name, or drive volume label. A zero-length string (<see langword="&quot;&quot;" />) is returned if <paramref name="PathName" /> is not found. </param>
    /// <param name="Attributes">Optional. Enumeration or numeric expression whose value specifies file attributes. If omitted, <see langword="Dir" /> returns files that match <paramref name="PathName" /> but have no attributes.</param>
    /// <returns>A string representing the name of a file, directory, or folder that matches a specified pattern or file attribute, or the volume label of a drive.</returns>
    public static string Dir(string PathName, FileAttribute Attributes = FileAttribute.Normal)
    {
      if (Attributes == FileAttribute.Volume)
      {
        StringBuilder stringBuilder = new StringBuilder(256);
        string str = (string) null;
        if (PathName.Length > 0)
        {
          str = Path.GetPathRoot(PathName);
          if ((int) str[checked (str.Length - 1)] != (int) Path.DirectorySeparatorChar)
            str += Conversions.ToString(Path.DirectorySeparatorChar);
        }
        string lpRootPathName = str;
        StringBuilder lpVolumeNameBuffer = stringBuilder;
        int nVolumeNameSize = 256;
        int num1 = 0;
        ref int local1 = ref num1;
        int num2 = 0;
        ref int local2 = ref num2;
        int num3 = 0;
        ref int local3 = ref num3;
        IntPtr num4= IntPtr.Zero;
        IntPtr lpFileSystemNameBuffer = num4;
        int nFileSystemNameSize = 0;
        if (NativeMethods.GetVolumeInformation(lpRootPathName, lpVolumeNameBuffer, nVolumeNameSize, ref local1, ref local2, ref local3, lpFileSystemNameBuffer, nFileSystemNameSize) != 0)
          return stringBuilder.ToString();
        return "";
      }
      FileAttributes Attributes1 = (FileAttributes) (Attributes | (FileAttribute) 128);
      return IOUtils.FindFirstFile(Assembly.GetCallingAssembly(), PathName, Attributes1);
    }

    /// <summary>Creates a new directory. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="MkDir" />. For more information, see <see cref="M:Ported.VisualBasic.FileIO.FileSystem.CreateDirectory(System.String)" />.</summary>
    /// <param name="Path">Required. <see langword="String" /> expression that identifies the directory to be created. The <paramref name="Path" /> may include the drive. If no drive is specified, <see langword="MkDir" /> creates the new directory on the current drive.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Path" /> is not specified or is empty.</exception>
    /// <exception cref="T:System.Security.SecurityException">Permission denied.</exception>
    /// <exception cref="T:System.IO.IOException">Directory already exists.</exception>
    public static void MkDir(string Path)
    {
      if (Path == null || Path.Length == 0)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty")), 52);
      if (Directory.Exists(Path))
        throw ExceptionUtils.VbMakeException(75);
      Directory.CreateDirectory(Path);
    }

    /// <summary>Removes an existing directory. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="RmDir" />. For more information, see <see cref="Overload:Ported.VisualBasic.FileIO.FileSystem.DeleteDirectory" />.</summary>
    /// <param name="Path">Required. <see langword="String" /> expression that identifies the directory or folder to be removed. <paramref name="Path" /> can include the drive. If no drive is specified, <see langword="RmDir" /> removes the directory on the current drive.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Path" /> is not specified or is empty.</exception>
    /// <exception cref="T:System.IO.IOException">Target directory contains files.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">Directory does not exist.</exception>
    public static void RmDir(string Path)
    {
      if (Path != null)
      {
        if (Path.Length != 0)
        {
          try
          {
            Directory.Delete(Path);
            return;
          }
          catch (DirectoryNotFoundException ex)
          {
            throw ExceptionUtils.VbMakeException((Exception) ex, 76);
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
            throw ExceptionUtils.VbMakeException(ex, 75);
          }
        }
      }
      throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty")), 52);
    }

    private static bool PathContainsWildcards(string Path)
    {
      return Path != null && (Path.IndexOf('*') != -1 || Path.IndexOf('?') != -1);
    }

    /// <summary>Copies a file. The <see cref="T:Ported.VisualBasic.FileIO.FileSystem" /> gives you better productivity and performance in file I/O operations than <see langword="FileCopy" />. See <see cref="M:Ported.VisualBasic.FileIO.FileSystem.CopyFile(System.String,System.String)" /> for more information.</summary>
    /// <param name="Source">Required. <see langword="String" /> expression that specifies the name of the file to be copied. <paramref name="Source" /> may include the directory or folder, and drive, of the source file.</param>
    /// <param name="Destination">Required. <see langword="String" /> expression that specifies the destination file name. <paramref name="Destination" /> may include the directory or folder, and drive, of the destination file.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Source" /> or <paramref name="Destination" /> is invalid or not specified.</exception>
    /// <exception cref="T:System.IO.IOException">File is already open.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">File does not exist.</exception>
    public static void FileCopy(string Source, string Destination)
    {
      if (Source == null || Source.Length == 0)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty1", new string[1]
        {
          nameof (Source)
        })), 52);
      if (Destination == null || Destination.Length == 0)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty1", new string[1]
        {
          nameof (Destination)
        })), 52);
      if (FileSystem.PathContainsWildcards(Source))
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Source)
        })), 52);
      if (FileSystem.PathContainsWildcards(Destination))
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Destination)
        })), 52);
      AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(Assembly.GetCallingAssembly());
      if (FileSystem.CheckFileOpen(assemblyData, Destination, OpenModeTypes.Output))
        throw ExceptionUtils.VbMakeException((Exception) new IOException(Utils.GetResourceString("FileSystem_FileAlreadyOpen1", new string[1]
        {
          Destination
        })), 55);
      if (FileSystem.CheckFileOpen(assemblyData, Source, OpenModeTypes.Input))
        throw ExceptionUtils.VbMakeException((Exception) new IOException(Utils.GetResourceString("FileSystem_FileAlreadyOpen1", new string[1]
        {
          Source
        })), 55);
      try
      {
        File.Copy(Source, Destination, true);
        File.SetAttributes(Destination, FileAttributes.Archive);
      }
      catch (FileNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 53);
      }
      catch (IOException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 55);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a <see langword="Date" /> value that indicates the date and time a file was written to. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileDateTime" />. For more information, see <see cref="M:Ported.VisualBasic.FileIO.FileSystem.GetFileInfo(System.String)" /></summary>
    /// <param name="PathName">Required. <see langword="String" /> expression that specifies a file name. <paramref name="PathName" /> may include the directory or folder, and the drive.</param>
    /// <returns>
    /// <see langword="Date" /> value that indicates the date and time a file was created or last modified. </returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="PathName" /> is invalid or contains wildcards.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">Target file does not exist.</exception>
    public static DateTime FileDateTime(string PathName)
    {
      if (FileSystem.PathContainsWildcards(PathName))
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (PathName)
        })), 52);
      if (File.Exists(PathName))
        return new FileInfo(PathName).LastWriteTime;
      throw new FileNotFoundException(Utils.GetResourceString("FileSystem_FileNotFound1", new string[1]
      {
        PathName
      }));
    }

    /// <summary>Returns a <see langword="Long" /> value that specifies the length of a file in bytes. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileLen" />. For more information, see <see cref="M:Ported.VisualBasic.FileIO.FileSystem.GetFileInfo(System.String)" />.</summary>
    /// <param name="PathName">Required. <see langword="String" /> expression that specifies a file. <paramref name="PathName" /> may include the directory or folder, and the drive.</param>
    /// <returns>
    /// <see langword="Long" /> value that specifies the length of a file in bytes. </returns>
    /// <exception cref="T:System.IO.FileNotFoundException">File does not exist.</exception>
    public static long FileLen(string PathName)
    {
      if (File.Exists(PathName))
        return new FileInfo(PathName).Length;
      throw new FileNotFoundException(Utils.GetResourceString("FileSystem_FileNotFound1", new string[1]
      {
        PathName
      }));
    }

    /// <summary>Returns a <see langword="FileAttribute" /> value that represents the attributes of a file, directory, or folder. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileAttribute" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="PathName">Required. <see langword="String" /> expression that specifies a file, directory, or folder name. <paramref name="PathName" /> can include the directory or folder, and the drive.</param>
    /// <returns>The value returned by <see langword="GetAttr" /> is the sum of the following enumeration values:ValueConstantDescription
    ///   <see langword="Normal" />
    /// 
    ///   <see langword="vbNormal" />
    /// Normal.
    ///   <see langword="ReadOnly" />
    /// 
    ///   <see langword="vbReadOnly" />
    /// Read-only.
    ///   <see langword="Hidden" />
    /// 
    ///   <see langword="vbHidden" />
    /// Hidden.
    ///   <see langword="System" />
    /// 
    ///   <see langword="vbSystem" />
    /// System file.
    ///   <see langword="Directory" />
    /// 
    ///   <see langword="vbDirectory" />
    /// Directory or folder.
    ///   <see langword="Archive" />
    /// 
    ///   <see langword="vbArchive" />
    /// File has changed since last backup.
    ///   <see langword="Alias" />
    /// 
    ///   <see langword="vbAlias" />
    /// File has a different name.These enumerations are specified by the Visual Basic language. The names can be used anywhere in your code in place of the actual values.</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="Pathname" /> is invalid or contains wildcards.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">Target file does not exist.</exception>
    public static FileAttribute GetAttr(string PathName)
    {
      char[] anyOf = new char[2]{ '*', '?' };
      if (PathName.IndexOfAny(anyOf) >= 0)
        throw ExceptionUtils.VbMakeException(52);
      FileInfo fileInfo = new FileInfo(PathName);
      if (fileInfo.Exists)
        return (FileAttribute) (fileInfo.Attributes & (FileAttributes) 63);
      DirectoryInfo directoryInfo = new DirectoryInfo(PathName);
      if (directoryInfo.Exists)
        return (FileAttribute) (directoryInfo.Attributes & (FileAttributes) 63);
      if (Path.GetFileName(PathName).Length == 0)
        throw ExceptionUtils.VbMakeException(52);
      throw new FileNotFoundException(Utils.GetResourceString("FileSystem_FileNotFound1", new string[1]
      {
        PathName
      }));
    }

    /// <summary>Deletes files from a disk. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Kill" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" /> .</summary>
    /// <param name="PathName">Required. <see langword="String" /> expression that specifies one or more file names to be deleted. <paramref name="PathName" /> can include the directory or folder, and the drive.</param>
    /// <exception cref="T:System.IO.IOException">Target file(s) open.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">Target file(s) not found.</exception>
    /// <exception cref="T:System.Security.SecurityException">Permission denied.</exception>
    public static void Kill(string PathName)
    {
      string path = Path.GetDirectoryName(PathName);
      string searchPattern;
      if (path == null || path.Length == 0)
      {
        path = Environment.CurrentDirectory;
        searchPattern = PathName;
      }
      else
        searchPattern = Path.GetFileName(PathName);
      FileInfo[] files = new DirectoryInfo(path).GetFiles(searchPattern);
      string str = path + Conversions.ToString(Path.PathSeparator);
      int num1=0;
      if (files != null)
      {
        int num2 = 0;
        int upperBound = files.GetUpperBound(0);
        int index = num2;
        while (index <= upperBound)
        {
          FileInfo fileInfo = files[index];
          if ((fileInfo.Attributes & (FileAttributes.Hidden | FileAttributes.System)) == (FileAttributes) 0)
          {
            string fullName = fileInfo.FullName;
            if (FileSystem.CheckFileOpen(ProjectData.GetProjectData().GetAssemblyData(Assembly.GetCallingAssembly()), fullName, OpenModeTypes.Any))
              throw ExceptionUtils.VbMakeException((Exception) new IOException(Utils.GetResourceString("FileSystem_FileAlreadyOpen1", new string[1]
              {
                fullName
              })), 55);
            try
            {
              File.Delete(fullName);
              checked { ++num1; }
            }
            catch (IOException ex)
            {
              throw ExceptionUtils.VbMakeException((Exception) ex, 55);
            }
            catch (Exception ex)
            {
              throw ex;
            }
          }
          checked { ++index; }
        }
      }
      if (num1 == 0)
        throw new FileNotFoundException(Utils.GetResourceString("KILL_NoFilesFound1", new string[1]
        {
          PathName
        }));
    }

    /// <summary>Sets attribute information for a file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="SetAttr" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="PathName">Required. <see langword="String" /> expression that specifies a file name. <paramref name="PathName" /> can include directory or folder, and drive.</param>
    /// <param name="Attributes">Required. Constant or numeric expression, whose sum specifies file attributes.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="PathName" /> invalid or does not exist.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Attribute" /> type is invalid.</exception>
    public static void SetAttr(string PathName, FileAttribute Attributes)
    {
      if (PathName == null || PathName.Length == 0)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PathNullOrEmpty")), 52);
      FileSystem.VB6CheckPathname(ProjectData.GetProjectData().GetAssemblyData(Assembly.GetCallingAssembly()), PathName, OpenMode.Input);
      if ((Attributes | FileAttribute.ReadOnly | FileAttribute.Hidden | FileAttribute.System | FileAttribute.Archive) != (FileAttribute.ReadOnly | FileAttribute.Hidden | FileAttribute.System | FileAttribute.Archive))
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Attributes)
        }));
      FileAttributes fileAttributes = (FileAttributes) Attributes;
      File.SetAttributes(PathName, fileAttributes);
    }

    private static bool UnsafeValidDrive(char cDrive)
    {
      return ((long) Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.GetLogicalDrives() & checked ((long) Math.Round(Math.Pow(2.0, unchecked ((double) checked ((int) cDrive - 65)))))) != 0L;
    }

    private static void ValidateAccess(OpenAccess Access)
    {
      if (Access != OpenAccess.Default && Access != OpenAccess.Read && (Access != OpenAccess.ReadWrite && Access != OpenAccess.Write))
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Access)
        }));
    }

    private static void ValidateShare(OpenShare Share)
    {
      if (Share != OpenShare.Default && Share != OpenShare.Shared && (Share != OpenShare.LockRead && Share != OpenShare.LockReadWrite) && Share != OpenShare.LockWrite)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Share)
        }));
    }

    private static void ValidateMode(OpenMode Mode)
    {
      if (Mode != OpenMode.Input && Mode != OpenMode.Output && (Mode != OpenMode.Random && Mode != OpenMode.Append) && Mode != OpenMode.Binary)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Mode)
        }));
    }

    /// <summary>Opens a file for input or output. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileOpen" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number. Use the <see langword="FreeFile" /> function to obtain the next available file number.</param>
    /// <param name="FileName">Required. <see langword="String" /> expression that specifies a file name—may include directory or folder, and drive.</param>
    /// <param name="Mode">Required. Enumeration specifying the file mode: <see langword="Append" />, <see langword="Binary" />, <see langword="Input" />, <see langword="Output" />, or <see langword="Random" />. For more information, see <see cref="T:Ported.VisualBasic.OpenMode" /> .</param>
    /// <param name="Access">Optional. Enumeration specifying the operations permitted on the open file: <see langword="Read" />, <see langword="Write" />, or <see langword="ReadWrite" />. Defaults to <see langword="ReadWrite" />. For more information, see <see cref="T:Ported.VisualBasic.OpenAccess" /> .</param>
    /// <param name="Share">Optional. Enumeration specifying the operations not permitted on the open file by other processes: <see langword="Shared" />, <see langword="Lock Read" />, <see langword="Lock Write" />, and <see langword="Lock Read Write" />. Defaults to <see langword="Lock Read Write" />. For more information, see <see cref="T:Ported.VisualBasic.OpenShare" /> .</param>
    /// <param name="RecordLength">Optional. Number less than or equal to 32,767 (bytes). For files opened for random access, this value is the record length. For sequential files, this value is the number of characters buffered.</param>
    /// <exception cref="T:System.ArgumentException">Invalid <see langword="Access" />, <see langword="Share" />, or <see langword="Mode" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see langword="WriteOnly" /> file is opened for <see langword="Input" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see langword="ReadOnly" /> file is opened for <see langword="Output" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <see langword="ReadOnly" /> file is opened for <see langword="Append" />.</exception>
    /// <exception cref="T:System.ArgumentException">Record length is negative (and not equal to -1).</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> is invalid (&lt;-1 or &gt;255), or <paramref name="FileNumber" /> is already in use.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileName" /> is already open, or <paramref name="FileName" /> is invalid.</exception>
    public static void FileOpen(int FileNumber, string FileName, OpenMode Mode, OpenAccess Access = OpenAccess.Default, OpenShare Share = OpenShare.Default, int RecordLength = -1)
    {
      try
      {
        FileSystem.ValidateMode(Mode);
        FileSystem.ValidateAccess(Access);
        FileSystem.ValidateShare(Share);
        if (FileNumber < 1 || FileNumber > (int) byte.MaxValue)
          throw ExceptionUtils.VbMakeException(52);
        FileSystem.vbIOOpenFile(Assembly.GetCallingAssembly(), FileNumber, FileName, Mode, Access, Share, RecordLength);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Concludes input/output (I/O) to a file opened using the <see langword="FileOpen" /> function. <see langword="My" /> gives you better productivity and performance in file I/O operations. See <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />  for more information.</summary>
    /// <param name="FileNumbers">Optional. Parameter array of 0 or more channels to be closed. </param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    public static void FileClose(params int[] FileNumbers)
    {
      try
      {
        AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(Assembly.GetCallingAssembly());
        if (FileNumbers == null || FileNumbers.Length == 0)
        {
          FileSystem.CloseAllFiles(assemblyData);
        }
        else
        {
          int num = 0;
          int upperBound = FileNumbers.GetUpperBound(0);
          int index = num;
          while (index <= upperBound)
          {
            FileSystem.InternalCloseFile(assemblyData, FileNumbers[index]);
            checked { ++index; }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static void ValidateGetPutRecordNumber(long RecordNumber)
    {
      if (RecordNumber < 1L && RecordNumber != -1L)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (RecordNumber)
        })), 63);
    }

    /// <summary>Reads data from an open disk file into a variable.  The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGetObject" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    public static void FileGetObject(int FileNumber, ref object Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).GetObject(ref Value, RecordNumber, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref ValueType Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <param name="ArrayIsDynamic">Optional. Applies only when writing an array. Specifies whether the array is to be treated as dynamic and whether an array descriptor describing the size and bounds of the array is necessary.</param>
    /// <param name="StringIsFixedLength">Optional. Applies only when writing a string. Specifies whether to write a two-byte descriptor for the string that describes the length. The default is <see langword="False" />.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref Array Value, long RecordNumber = -1, bool ArrayIsDynamic = false, bool StringIsFixedLength = false)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber, ArrayIsDynamic, StringIsFixedLength);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" /></summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref bool Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref byte Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref short Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref int Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref long Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref char Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref float Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref double Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref Decimal Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <param name="StringIsFixedLength">Optional. Applies only when writing a string. Specifies whether to write a two-byte descriptor for the string that describes the length. The default is <see langword="False" />.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref string Value, long RecordNumber = -1, bool StringIsFixedLength = false)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber, StringIsFixedLength);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open disk file into a variable. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FileGet" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name into which data is read.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which reading starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileGet(int FileNumber, ref DateTime Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Get(ref Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file.  The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePutObject" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    public static void FilePutObject(int FileNumber, object Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).PutObject(Value, RecordNumber, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    [Obsolete("This member has been deprecated. Please use FilePutObject to write Object types, or coerce FileNumber and RecordNumber to Integer for writing non-Object types. http://go.microsoft.com/fwlink/?linkid=14202")]
    public static void FilePut(object FileNumber, object Value, object RecordNumber = null)
    {
      if (RecordNumber is null) RecordNumber = -1;
      throw new ArgumentException(Utils.GetResourceString("UseFilePutObject"));
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, ValueType Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <param name="ArrayIsDynamic">Optional. Applies only when writing an array. Specifies whether the array is to be treated as dynamic, and whether to write an array descriptor for the string that describes the length. </param>
    /// <param name="StringIsFixedLength">Optional. Applies only when writing a string. Specifies whether to write a two-byte string length descriptor for the string to the file. The default is <see langword="False" />.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, Array Value, long RecordNumber = -1, bool ArrayIsDynamic = false, bool StringIsFixedLength = false)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber, ArrayIsDynamic, StringIsFixedLength);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, bool Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, byte Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, short Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, int Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, long Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, char Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, float Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, double Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, Decimal Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />..</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <param name="StringIsFixedLength">Optional. Applies only when writing a string. Specifies whether to write a two-byte string length descriptor for the string to the file. The default is <see langword="False" />.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, string Value, long RecordNumber = -1, bool StringIsFixedLength = false)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber, StringIsFixedLength);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data from a variable to a disk file. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="FilePut" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Valid variable name that contains data written to disk.</param>
    /// <param name="RecordNumber">Optional. Record number (<see langword="Random" /> mode files) or byte number (<see langword="Binary" /> mode files) at which writing starts.</param>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="RecordNumber" /> &lt; 1 and not equal to -1.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FilePut(int FileNumber, DateTime Value, long RecordNumber = -1)
    {
      try
      {
        FileSystem.ValidateGetPutRecordNumber(RecordNumber);
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber, OpenModeTypes.Random | OpenModeTypes.Binary).Put(Value, RecordNumber);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes display-formatted data to a sequential file.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Output">Optional. Zero or more comma-delimited expressions to write to a file.The <paramref name="Output" /> argument settings are: <see langword="T:System.IO.IOException" />: File mode is invalid.<see langword="T:System.IO.IOException" />: <paramref name="FileNumber" /> does not exist.</param>
    public static void Print(int FileNumber, params object[] Output)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Print(Output);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes display-formatted data to a sequential file.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Output">Optional. Zero or more comma-delimited expressions to write to a file.The <paramref name="Output" /> argument settings are: <see langword="T:System.IO.IOException" />: File mode is invalid.<see langword="T:System.IO.IOException" />: <paramref name="FileNumber" /> does not exist.</param>
    public static void PrintLine(int FileNumber, params object[] Output)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).PrintLine(Output);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref object Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref bool Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref byte Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref short Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref int Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref long Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref char Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref float Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref double Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref Decimal Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref string Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads data from an open sequential file and assigns the data to variables.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Value">Required. Variable that is assigned the values read from the file—cannot be an array or object variable.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Input(int FileNumber, ref DateTime Value)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Input(ref Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data to a sequential file. Data written with <see langword="Write" /> is usually read from a file by using <see langword="Input" />.</summary>
    /// <param name="FileNumber">Required. An <see langword="Integer" /> expression that contains any valid file number.</param>
    /// <param name="Output">Optional. One or more comma-delimited expressions to write to a file.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Write(int FileNumber, params object[] Output)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).WriteHelper(Output);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Writes data to a sequential file. Data written with <see langword="Write" /> is usually read from a file by using <see langword="Input" />.</summary>
    /// <param name="FileNumber">Required. An <see langword="Integer" /> expression that contains any valid file number.</param>
    /// <param name="Output">Optional. One or more comma-delimited expressions to write to a file.</param>
    public static void WriteLine(int FileNumber, params object[] Output)
    {
      try
      {
        FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).WriteLineHelper(Output);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns <see langword="String" /> value that contains characters from a file opened in <see langword="Input" /> or <see langword="Binary" /> mode. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="InputString" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="CharCount">Required. Any valid numeric expression specifying the number of characters to read.</param>
    /// <returns>Returns <see langword="String" /> value that contains characters from a file opened in <see langword="Input" /> or <see langword="Binary" /> mode. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="InputString" />. </returns>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="CharCount" /> &lt; 0 or &gt; 214.</exception>
    public static string InputString(int FileNumber, int CharCount)
    {
      try
      {
        if (CharCount < 0 || (double) CharCount > 1073741823.5)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (CharCount)
          }));
        VB6File channelObj = FileSystem.GetChannelObj(Assembly.GetCallingAssembly(), FileNumber);
        channelObj.Lock();
        try
        {
          return channelObj.InputString(CharCount);
        }
        finally
        {
          channelObj.Unlock();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Reads a single line from an open sequential file and assigns it to a <see langword="String" /> variable.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <returns>Reads a single line from an open sequential file and assigns it to a <see langword="String" /> variable.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">End of file reached.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    public static string LineInput(int FileNumber)
    {
      VB6File stream = FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber);
      FileSystem.CheckInputCapable(stream);
      if (stream.EOF())
        throw ExceptionUtils.VbMakeException(62);
      return stream.LineInput();
    }

    /// <summary>Controls access by other processes to all or part of a file opened by using the <see langword="Open" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Lock" /> and <see langword="Unlock" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Lock(int FileNumber)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Lock();
    }

    /// <summary>Controls access by other processes to all or part of a file opened by using the <see langword="Open" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Lock" /> and <see langword="Unlock" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Record">Optional. Number of the only record or byte to lock or unlock</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Lock(int FileNumber, long Record)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Lock(Record);
    }

    /// <summary>Controls access by other processes to all or part of a file opened by using the <see langword="Open" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Lock" /> and <see langword="Unlock" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="FromRecord">Optional. Number of the first record or byte to lock or unlock.</param>
    /// <param name="ToRecord">Optional. Number of the last record or byte to lock or unlock.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Lock(int FileNumber, long FromRecord, long ToRecord)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Lock(FromRecord, ToRecord);
    }

    /// <summary>Controls access by other processes to all or part of a file opened by using the <see langword="Open" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Lock" /> and <see langword="Unlock" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Unlock(int FileNumber)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Unlock();
    }

    /// <summary>Controls access by other processes to all or part of a file opened by using the <see langword="Open" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Lock" /> and <see langword="Unlock" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="Record">Optional. Number of the only record or byte to lock or unlock</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Unlock(int FileNumber, long Record)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Unlock(Record);
    }

    /// <summary>Controls access by other processes to all or part of a file opened by using the <see langword="Open" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Lock" /> and <see langword="Unlock" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="FromRecord">Optional. Number of the first record or byte to lock or unlock.</param>
    /// <param name="ToRecord">Optional. Number of the last record or byte to lock or unlock.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Unlock(int FileNumber, long FromRecord, long ToRecord)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Unlock(FromRecord, ToRecord);
    }

    /// <summary>Assigns an output line width to a file opened by using the <see langword="FileOpen" /> function.</summary>
    /// <param name="FileNumber">Required. Any valid file number.</param>
    /// <param name="RecordWidth">Required. Numeric expression in the range 0–255, inclusive, which indicates how many characters appear on a line before a new line is started. If <paramref name="RecordWidth" /> equals 0, there is no limit to the length of a line. The default value for <paramref name="RecordWidth" /> is 0.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void FileWidth(int FileNumber, int RecordWidth)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).SetWidth(RecordWidth);
    }

    /// <summary>Returns an <see langword="Integer" /> value that represents the next file number available for use by the <see langword="FileOpen" /> function.</summary>
    /// <returns>Returns an <see langword="Integer" /> value that represents the next file number available for use by the <see langword="FileOpen" /> function.</returns>
    /// <exception cref="T:System.IO.IOException">More than 255 files are in use.</exception>
    public static int FreeFile()
    {
      AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(Assembly.GetCallingAssembly());
      int lChannel = 1;
      while (assemblyData.GetChannelObj(lChannel) != null)
      {
        checked { ++lChannel; }
        if (lChannel > (int) byte.MaxValue)
          throw ExceptionUtils.VbMakeException(67);
      }
      return lChannel;
    }

    /// <summary>Returns a <see langword="Long" /> specifying the current read/write position in a file opened by using the <see langword="FileOpen" /> function, or sets the position for the next read/write operation in a file opened by using the <see langword="FileOpen" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Seek" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. An <see langword="Integer" /> that contains a valid file number.</param>
    /// <param name="Position">Required. Number in the range 1–2,147,483,647, inclusive, that indicates where the next read/write operation should occur.</param>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static void Seek(int FileNumber, long Position)
    {
      FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Seek(Position);
    }

    /// <summary>Returns a <see langword="Long" /> specifying the current read/write position in a file opened by using the <see langword="FileOpen" /> function, or sets the position for the next read/write operation in a file opened by using the <see langword="FileOpen" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Seek" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. An <see langword="Integer" /> that contains a valid file number.</param>
    /// <returns>Returns a <see langword="Long" /> specifying the current read/write position in a file opened by using the <see langword="FileOpen" /> function, or sets the position for the next read/write operation in a file opened by using the <see langword="FileOpen" /> function. </returns>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static long Seek(int FileNumber)
    {
      return FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).Seek();
    }

    /// <summary>Returns a Boolean value <see langword="True" /> when the end of a file opened for <see langword="Random" /> or sequential <see langword="Input" /> has been reached.</summary>
    /// <param name="FileNumber">Required. An <see langword="Integer" /> that contains any valid file number.</param>
    /// <returns>Returns a Boolean value <see langword="True" /> when the end of a file opened for <see langword="Random" /> or sequential <see langword="Input" /> has been reached.</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static bool EOF(int FileNumber)
    {
      return FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).EOF();
    }

    /// <summary>Returns a <see langword="Long" /> value that specifies the current read/write position in an open file.</summary>
    /// <param name="FileNumber">Required. Any valid <see langword="Integer" /> file number.</param>
    /// <returns>Returns a <see langword="Long" /> value that specifies the current read/write position in an open file.</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static long Loc(int FileNumber)
    {
      return FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).LOC();
    }

    /// <summary>Returns a <see langword="Long" /> representing the size, in bytes, of a file opened by using the <see langword="FileOpen" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="LOF" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="FileNumber">Required. An <see langword="Integer" /> that contains a valid file number.</param>
    /// <returns>Returns a <see langword="Long" /> representing the size, in bytes, of a file opened by using the <see langword="FileOpen" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="LOF" />.</returns>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="FileNumber" /> does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">File mode is invalid.</exception>
    public static long LOF(int FileNumber)
    {
      return FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).LOF();
    }

    /// <summary>Used with the <see langword="Print" /> or <see langword="PrintLine" /> functions to position output.</summary>
    /// <returns>Used with the <see langword="Print" /> or <see langword="PrintLine" /> functions to position output.</returns>
    public static TabInfo TAB()
    {
      TabInfo tabInfo;
      tabInfo.Column = (short) -1;
      return tabInfo;
    }

    /// <summary>Used with the <see langword="Print" /> or <see langword="PrintLine" /> functions to position output.</summary>
    /// <param name="Column">Optional. The column number moved to before displaying or printing the next expression in a list. If omitted, <see langword="TAB" /> moves the insertion point to the start of the next print zone. </param>
    /// <returns>Used with the <see langword="Print" /> or <see langword="PrintLine" /> functions to position output.</returns>
    public static TabInfo TAB(short Column)
    {
      if (Column < (short) 1)
        Column = (short) 1;
      TabInfo tabInfo;
      tabInfo.Column = Column;
      return tabInfo;
    }

    /// <summary>Used with the <see langword="Print" /> or <see langword="PrintLine" /> function to position output.</summary>
    /// <param name="Count">Required. The number of spaces to insert before displaying or printing the next expression in a list.</param>
    /// <returns>Used with the <see langword="Print" /> or <see langword="PrintLine" /> function to position output.</returns>
    public static SpcInfo SPC(short Count)
    {
      if (Count < (short) 1)
        Count = (short) 0;
      SpcInfo spcInfo;
      spcInfo.Count = Count;
      return spcInfo;
    }

    /// <summary>Returns an enumeration representing the file mode for files opened using the <see langword="FileOpen" /> function. The <see cref="T:Ported.VisualBasic.FileIO.FileSystem" /> gives you better productivity and performance in file I/O operations than the <see langword="FileAttr " />function. See <see cref="M:Ported.VisualBasic.FileIO.FileSystem.GetFileInfo(System.String)" /> for more information.</summary>
    /// <param name="FileNumber">Required. <see langword="Integer" />. Any valid file number.</param>
    /// <returns>The following enumeration values indicate the file access mode:ValueMode1
    ///   <see langword="OpenMode.Input" />
    /// 2
    ///   <see langword="OpenMode.Output" />
    /// 4
    ///   <see langword="OpenMode.Random" />
    /// 8
    ///   <see langword="OpenMode.Append" />
    /// 32
    ///   <see langword="OpenMode.Binary" />
    /// </returns>
    public static OpenMode FileAttr(int FileNumber)
    {
      return FileSystem.GetStream(Assembly.GetCallingAssembly(), FileNumber).GetMode();
    }

    /// <summary>Closes all disk files opened by using the <see langword="FileOpen" /> function. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Reset" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    public static void Reset()
    {
      FileSystem.CloseAllFiles(Assembly.GetCallingAssembly());
    }

    /// <summary>Renames a disk file or directory. The <see langword="My" /> feature gives you better productivity and performance in file I/O operations than <see langword="Rename" />. For more information, see <see cref="T:Ported.VisualBasic.FileIO.FileSystem" />.</summary>
    /// <param name="OldPath">Required. <see langword="String" /> expression that specifies the existing file name and location. <paramref name="OldPath" /> may include the directory, and drive, of the file.</param>
    /// <param name="NewPath">Required. <see langword="String" /> expression that specifies the new file name and location. <paramref name="NewPath" /> may include directory and drive of the destination location. The file name specified by <paramref name="NewPath" /> cannot already exist.</param>
    /// <exception cref="T:System.ArgumentException">Path is invalid.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    /// <paramref name="OldPath" /> file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="NewPath" /> file already exists.</exception>
    /// <exception cref="T:System.IO.IOException">Access is invalid.</exception>
    /// <exception cref="T:System.IO.IOException">Cannot rename to different device.</exception>
    public static void Rename(string OldPath, string NewPath)
    {
      AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(Assembly.GetCallingAssembly());
      OldPath = FileSystem.VB6CheckPathname(assemblyData, OldPath, (OpenMode) (-1));
      NewPath = FileSystem.VB6CheckPathname(assemblyData, NewPath, (OpenMode) (-1));
      new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, OldPath).Demand();
      new FileIOPermission(FileIOPermissionAccess.Write, NewPath).Demand();
      if (Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.MoveFile(OldPath, NewPath) != 0)
        return;
      switch (Marshal.GetLastWin32Error())
      {
        case 2:
          throw ExceptionUtils.VbMakeException(53);
        case 12:
          throw ExceptionUtils.VbMakeException(75);
        case 17:
          throw ExceptionUtils.VbMakeException(74);
        case 80:
        case 183:
          throw ExceptionUtils.VbMakeException(58);
        default:
          throw ExceptionUtils.VbMakeException(5);
      }
    }

    private static VB6File GetStream(Assembly assem, int FileNumber)
    {
      return FileSystem.GetStream(assem, FileNumber, OpenModeTypes.Input | OpenModeTypes.Output | OpenModeTypes.Random | OpenModeTypes.Append | OpenModeTypes.Binary);
    }

    private static VB6File GetStream(Assembly assem, int FileNumber, OpenModeTypes mode)
    {
      if (FileNumber < 1 || FileNumber > (int) byte.MaxValue)
        throw ExceptionUtils.VbMakeException(52);
      VB6File channelObj = FileSystem.GetChannelObj(assem, FileNumber);
      if ((FileSystem.OpenModeTypesFromOpenMode(channelObj.GetMode()) | mode) == ~OpenModeTypes.Any)
        throw ExceptionUtils.VbMakeException(54);
      return channelObj;
    }

    private static OpenModeTypes OpenModeTypesFromOpenMode(OpenMode om)
    {
      switch (om)
      {
        case (OpenMode) (-1):
          return OpenModeTypes.Any;
        case OpenMode.Input:
          return OpenModeTypes.Input;
        case OpenMode.Output:
          return OpenModeTypes.Output;
        case OpenMode.Random:
          return OpenModeTypes.Random;
        case OpenMode.Append:
          return OpenModeTypes.Append;
        case OpenMode.Binary:
          return OpenModeTypes.Binary;
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"), nameof (om));
      }
    }

    internal static void CloseAllFiles(Assembly assem)
    {
      FileSystem.CloseAllFiles(ProjectData.GetProjectData().GetAssemblyData(assem));
    }

    internal static void CloseAllFiles(AssemblyData oAssemblyData)
    {
      int FileNumber = 1;
      do
      {
        FileSystem.InternalCloseFile(oAssemblyData, FileNumber);
        checked { ++FileNumber; }
      }
      while (FileNumber <= (int) byte.MaxValue);
    }

    private static void InternalCloseFile(AssemblyData oAssemblyData, int FileNumber)
    {
      if (FileNumber == 0)
      {
        FileSystem.CloseAllFiles(oAssemblyData);
      }
      else
      {
        VB6File channelOrNull = FileSystem.GetChannelOrNull(oAssemblyData, FileNumber);
        if (channelOrNull == null)
          return;
        oAssemblyData.SetChannelObj(FileNumber, (VB6File) null);
        channelOrNull?.CloseFile();
      }
    }

    internal static string VB6CheckPathname(AssemblyData oAssemblyData, string sPath, OpenMode mode)
    {
      if (sPath.IndexOf('?') != -1 || sPath.IndexOf('*') != -1)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidPathChars1", new string[1]
        {
          sPath
        }));
      string fullName = new FileInfo(sPath).FullName;
      if (FileSystem.CheckFileOpen(oAssemblyData, fullName, FileSystem.OpenModeTypesFromOpenMode(mode)))
        throw ExceptionUtils.VbMakeException(55);
      return fullName;
    }

    internal static bool CheckFileOpen(AssemblyData oAssemblyData, string sPath, OpenModeTypes NewFileMode)
    {
      int maxValue = (int) byte.MaxValue;
      int num1 = 1;
      int num2 = maxValue;
      int FileNumber = num1;
      while (FileNumber <= num2)
      {
        VB6File channelOrNull = FileSystem.GetChannelOrNull(oAssemblyData, FileNumber);
        if (channelOrNull != null)
        {
          OpenMode mode = channelOrNull.GetMode();
          if (string.Compare(sPath, channelOrNull.GetAbsolutePath(), StringComparison.OrdinalIgnoreCase) == 0 && (NewFileMode == OpenModeTypes.Any || (NewFileMode | (OpenModeTypes) mode) != OpenModeTypes.Input && (NewFileMode | (OpenModeTypes) mode | OpenModeTypes.Binary | OpenModeTypes.Random) != (OpenModeTypes.Random | OpenModeTypes.Binary)))
            return true;
        }
        checked { ++FileNumber; }
      }
      return false;
    }

    private static void vbIOOpenFile(Assembly assem, int FileNumber, string FileName, OpenMode Mode, OpenAccess Access, OpenShare Share, int RecordLength)
    {
      AssemblyData assemblyData = ProjectData.GetProjectData().GetAssemblyData(assem);
      if (FileSystem.GetChannelOrNull(assemblyData, FileNumber) != null)
        throw ExceptionUtils.VbMakeException(55);
      if (FileName == null || FileName.Length == 0)
        throw ExceptionUtils.VbMakeException(75);
      FileName = new FileInfo(FileName).FullName;
      if (FileSystem.CheckFileOpen(assemblyData, FileName, FileSystem.OpenModeTypesFromOpenMode(Mode)))
        throw ExceptionUtils.VbMakeException(55);
      if (RecordLength != -1 && RecordLength <= 0)
        throw ExceptionUtils.VbMakeException(5);
      if (Mode == OpenMode.Binary)
        RecordLength = 1;
      else if (RecordLength == -1)
        RecordLength = Mode != OpenMode.Random ? 512 : 128;
      if (Share == OpenShare.Default)
        Share = OpenShare.LockReadWrite;
      VB6File oFile;
      switch (Mode)
      {
        case OpenMode.Input:
          if (Access != OpenAccess.Read && Access != OpenAccess.Default)
            throw new ArgumentException(Utils.GetResourceString("FileSystem_IllegalInputAccess"));
          oFile = (VB6File) new VB6InputFile(FileName, Share);
          break;
        case OpenMode.Output:
          if (Access != OpenAccess.Write && Access != OpenAccess.Default)
            throw new ArgumentException(Utils.GetResourceString("FileSystem_IllegalOutputAccess"));
          oFile = (VB6File) new VB6OutputFile(FileName, Share, false);
          break;
        case OpenMode.Random:
          if (Access == OpenAccess.Default)
            Access = OpenAccess.ReadWrite;
          oFile = (VB6File) new VB6RandomFile(FileName, Access, Share, RecordLength);
          break;
        case OpenMode.Append:
          if (Access != OpenAccess.Write && Access != OpenAccess.ReadWrite && Access != OpenAccess.Default)
            throw new ArgumentException(Utils.GetResourceString("FileSystem_IllegalAppendAccess"));
          oFile = (VB6File) new VB6OutputFile(FileName, Share, true);
          break;
        case OpenMode.Binary:
          if (Access == OpenAccess.Default)
            Access = OpenAccess.ReadWrite;
          oFile = (VB6File) new VB6BinaryFile(FileName, Access, Share);
          break;
        default:
          throw ExceptionUtils.VbMakeException(51);
      }
      FileSystem.AddFileToList(assemblyData, FileNumber, oFile);
    }

    private static void AddFileToList(AssemblyData oAssemblyData, int FileNumber, VB6File oFile)
    {
      if (oFile == null)
        throw ExceptionUtils.VbMakeException(51);
      oFile.OpenFile();
      oAssemblyData.SetChannelObj(FileNumber, oFile);
    }

    internal static VB6File GetChannelObj(Assembly assem, int FileNumber)
    {
      VB6File channelOrNull = FileSystem.GetChannelOrNull(ProjectData.GetProjectData().GetAssemblyData(assem), FileNumber);
      if (channelOrNull == null)
        throw ExceptionUtils.VbMakeException(52);
      return channelOrNull;
    }

    private static VB6File GetChannelOrNull(AssemblyData oAssemblyData, int FileNumber)
    {
      return oAssemblyData.GetChannelObj(FileNumber);
    }

    private static void CheckInputCapable(VB6File oFile)
    {
      if (!oFile.CanInput())
        throw ExceptionUtils.VbMakeException(54);
    }

    internal enum vbFileType
    {
      vbPrintFile,
      vbWriteFile,
    }
  }
}

*/