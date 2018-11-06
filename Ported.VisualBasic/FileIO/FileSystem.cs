// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.FileSystem
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Provides properties and methods for working with drives, files, and directories.</summary>
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class FileSystem
  {
    private static readonly char[] m_SeparatorChars = new char[4]
    {
      Path.DirectorySeparatorChar,
      Path.AltDirectorySeparatorChar,
      Path.VolumeSeparatorChar,
      Path.PathSeparator
    };
    private const NativeMethods.ShFileOperationFlags m_SHELL_OPERATION_FLAGS_BASE = NativeMethods.ShFileOperationFlags.FOF_NOCONFIRMMKDIR | NativeMethods.ShFileOperationFlags.FOF_NO_CONNECTED_ELEMENTS;
    private const NativeMethods.ShFileOperationFlags m_SHELL_OPERATION_FLAGS_HIDE_UI = NativeMethods.ShFileOperationFlags.FOF_SILENT | NativeMethods.ShFileOperationFlags.FOF_NOCONFIRMATION;
    private const int m_MOVEFILEEX_FLAGS = 11;

    /// <summary>Returns a read-only collection of all available drive names.</summary>
    /// <returns>A read-only collection of all available drives as <see cref="T:System.IO.DriveInfo" /> objects.</returns>
    public static ReadOnlyCollection<DriveInfo> Drives
    {
      get
      {
        Collection<DriveInfo> collection = new Collection<DriveInfo>();
        DriveInfo[] drives = DriveInfo.GetDrives();
        int index = 0;
        while (index < drives.Length)
        {
          DriveInfo driveInfo = drives[index];
          collection.Add(driveInfo);
          checked { ++index; }
        }
        return new ReadOnlyCollection<DriveInfo>((IList<DriveInfo>) collection);
      }
    }

    /// <summary>Gets or sets the current directory.</summary>
    /// <returns>The current directory for file I/O operations.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is not valid.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static string CurrentDirectory
    {
      get
      {
        return FileSystem.NormalizePath(Directory.GetCurrentDirectory());
      }
      set
      {
        Directory.SetCurrentDirectory(value);
      }
    }

    /// <summary>Combines two paths and returns a properly formatted path.</summary>
    /// <param name="baseDirectory">
    /// <see langword="String" />. First path to be combined. </param>
    /// <param name="relativePath">
    /// <see langword="String" />. Second path to be combined. </param>
    /// <returns>The combination of the specified paths.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="baseDirectory" /> or <paramref name="relativePath" /> are malformed paths.</exception>
    public static string CombinePath(string baseDirectory, string relativePath)
    {
      if (Operators.CompareString(baseDirectory, "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (baseDirectory), "General_ArgumentEmptyOrNothing_Name", nameof (baseDirectory));
      if (Operators.CompareString(relativePath, "", false) == 0)
        return baseDirectory;
      baseDirectory = Path.GetFullPath(baseDirectory);
      return FileSystem.NormalizePath(Path.Combine(baseDirectory, relativePath));
    }

    /// <summary>Returns <see langword="True" /> if the specified directory exists.</summary>
    /// <param name="directory">Path of the directory. </param>
    /// <returns>
    /// <see langword="True" /> if the directory exists; otherwise <see langword="False" />.</returns>
    public static bool DirectoryExists(string directory)
    {
      return Directory.Exists(directory);
    }

    /// <summary>Returns <see langword="True" /> if the specified file exists.</summary>
    /// <param name="file">Name and path of the file. </param>
    /// <returns>Returns <see langword="True" /> if the file exists; otherwise this method returns <see langword="False" />.</returns>
    /// <exception cref="T:System.ArgumentException">The name of the file ends with a backslash (\).</exception>
    public static bool FileExists(string file)
    {
      if (!string.IsNullOrEmpty(file) && file.EndsWith(Conversions.ToString(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase) | file.EndsWith(Conversions.ToString(Path.AltDirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
        return false;
      return File.Exists(file);
    }

    /// <summary>Returns a read-only collection of strings representing the names of files containing the specified text.</summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <param name="containsText">The search text.</param>
    /// <param name="ignoreCase">
    /// <see langword="True" /> if the search should be case-sensitive; otherwise <see langword="False" />. Default is <see langword="True" />.</param>
    /// <param name="searchType">Whether to include subfolders. Default is <see langword="SearchOption.SearchTopLevelOnly" />. </param>
    /// <returns>Read-only collection of the names of files containing the specified text..</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The specified directory points to an existing file.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">The specified directory path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static ReadOnlyCollection<string> FindInFiles(string directory, string containsText, bool ignoreCase, SearchOption searchType)
    {
      return FileSystem.FindInFiles(directory, containsText, ignoreCase, searchType, (string[]) null);
    }

    /// <summary>Returns a read-only collection of strings representing the names of files containing the specified text.</summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <param name="containsText">The search text.</param>
    /// <param name="ignoreCase">
    /// <see langword="True" /> if the search should be case-sensitive; otherwise <see langword="False" />. Default is <see langword="True" />.</param>
    /// <param name="searchType">Whether to include subfolders. Default is <see langword="SearchOption.SearchTopLevelOnly" />. </param>
    /// <param name="fileWildcards">Pattern to be matched.</param>
    /// <returns>Read-only collection of the names of files containing the specified text..</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The specified directory points to an existing file.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">The specified directory path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static ReadOnlyCollection<string> FindInFiles(string directory, string containsText, bool ignoreCase, SearchOption searchType, params string[] fileWildcards)
    {
      ReadOnlyCollection<string> filesOrDirectories = FileSystem.FindFilesOrDirectories(FileSystem.FileOrDirectory.File, directory, searchType, fileWildcards);
      if (Operators.CompareString(containsText, "", false) == 0)
        return filesOrDirectories;
      Collection<string> collection = new Collection<string>();
      IEnumerator<string> enumerator=Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<string>();
      try
      {
        enumerator = filesOrDirectories.GetEnumerator();
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          if (FileSystem.FileContainsText(current, containsText, ignoreCase))
            collection.Add(current);
        }
      }
      finally
      {
        enumerator?.Dispose();
      }
      return new ReadOnlyCollection<string>((IList<string>) collection);
    }

    /// <summary>Returns a collection of strings representing the path names of subdirectories within a directory.</summary>
    /// <param name="directory">Name and path of directory. </param>
    /// <returns>Read-only collection of the path names of subdirectories within the specified directory..</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The specified directory points to an existing file.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static ReadOnlyCollection<string> GetDirectories(string directory)
    {
      return FileSystem.FindFilesOrDirectories(FileSystem.FileOrDirectory.Directory, directory, SearchOption.SearchTopLevelOnly, (string[]) null);
    }

    /// <summary>Returns a collection of strings representing the path names of subdirectories within a directory.</summary>
    /// <param name="directory">Name and path of directory. </param>
    /// <param name="searchType">Whether to include subfolders. Default is <see langword="SearchOption.SearchTopLevelOnly" />. </param>
    /// <param name="wildcards">Pattern to match names. </param>
    /// <returns>Read-only collection of the path names of subdirectories within the specified directory.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">One or more of the specified wildcard characters is <see langword="Nothing" />, an empty string, or contains only spaces.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The specified directory points to an existing file.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static ReadOnlyCollection<string> GetDirectories(string directory, SearchOption searchType, params string[] wildcards)
    {
      return FileSystem.FindFilesOrDirectories(FileSystem.FileOrDirectory.Directory, directory, searchType, wildcards);
    }

    /// <summary>Returns a <see cref="T:System.IO.DirectoryInfo" /> object for the specified path.</summary>
    /// <param name="directory">
    /// <see langword="String" />. Path of directory. </param>
    /// <returns>
    /// <see cref="T:System.IO.DirectoryInfo" /> object for the specified path.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">The directory path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path</exception>
    public static DirectoryInfo GetDirectoryInfo(string directory)
    {
      return new DirectoryInfo(directory);
    }

    /// <summary>Returns a <see cref="T:System.IO.DriveInfo" /> object for the specified drive.</summary>
    /// <param name="drive">Drive to be examined. </param>
    /// <returns>
    /// <see cref="T:System.IO.DriveInfo" /> object for the specified drive.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="drive" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path</exception>
    public static DriveInfo GetDriveInfo(string drive)
    {
      return new DriveInfo(drive);
    }

    /// <summary>Returns a <see cref="T:System.IO.FileInfo" /> object for the specified file.</summary>
    /// <param name="file">Name and path of the file. </param>
    /// <returns>
    /// <see cref="T:System.IO.FileInfo" /> object for the specified file</returns>
    /// <exception cref="T:System.ArgumentException">The path name is malformed. For example, it contains invalid characters or is only white space. The file name has a trailing slash mark.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.NotSupportedException">The path contains a colon in the middle of the string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path is too long.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks ACL (access control list) access to the file.</exception>
    public static FileInfo GetFileInfo(string file)
    {
      file = FileSystem.NormalizeFilePath(file, nameof (file));
      return new FileInfo(file);
    }

    /// <summary>Returns a read-only collection of strings representing the names of files within a directory.</summary>
    /// <param name="directory">Directory to be searched. </param>
    /// <returns>Read-only collection of file names from the specified directory.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory to search does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="directory" /> points to an existing file.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static ReadOnlyCollection<string> GetFiles(string directory)
    {
      return FileSystem.FindFilesOrDirectories(FileSystem.FileOrDirectory.File, directory, SearchOption.SearchTopLevelOnly, (string[]) null);
    }

    /// <summary>Returns a read-only collection of strings representing the names of files within a directory.</summary>
    /// <param name="directory">Directory to be searched. </param>
    /// <param name="searchType">Whether to include subfolders. Default is <see langword="SearchOption.SearchTopLevelOnly" />. </param>
    /// <param name="wildcards">Pattern to be matched. </param>
    /// <returns>Read-only collection of file names from the specified directory.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory to search does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="directory" /> points to an existing file.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public static ReadOnlyCollection<string> GetFiles(string directory, SearchOption searchType, params string[] wildcards)
    {
      return FileSystem.FindFilesOrDirectories(FileSystem.FileOrDirectory.File, directory, searchType, wildcards);
    }

    /// <summary>Parses the file name out of the path provided.</summary>
    /// <param name="path">Required. Path to be parsed. <see langword="String" />.</param>
    /// <returns>The file name from the specified path.</returns>
    public static string GetName(string path)
    {
      return Path.GetFileName(path);
    }

    /// <summary>Returns the parent path of the provided path.</summary>
    /// <param name="path">Path to be examined. </param>
    /// <returns>The parent path of the provided path.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">Path does not have a parent path because it is a root path.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    public static string GetParentPath(string path)
    {
      Path.GetFullPath(path);
      if (FileSystem.IsRoot(path))
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (path), "IO_GetParentPathIsRoot_Path", path);
      return Path.GetDirectoryName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
    }

    /// <summary>Creates a uniquely named zero-byte temporary file on disk and returns the full path of that file.</summary>
    /// <returns>
    /// <see langword="String" /> containing the full path of the temporary file.</returns>
    public static string GetTempFileName()
    {
      return Path.GetTempFileName();
    }

    /// <summary>The <see langword="OpenTextFieldParser" /> method allows you to create a <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object,  which provides a way to easily and efficiently parse structured text files, such as logs. The <see langword="TextFieldParser" /> object can be used to read both delimited and fixed-width files.</summary>
    /// <param name="file">The file to be opened with the <see langword="TextFieldParser" />.</param>
    /// <returns>
    /// <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> to read the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:Ported.VisualBasic.FileIO.MalformedLineException">A row cannot be parsed using the specified format. The exception message specifies the line causing the exception, while the <see cref="P:Ported.VisualBasic.FileIO.TextFieldParser.ErrorLine" />  property is assigned the text contained in the line.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static TextFieldParser OpenTextFieldParser(string file)
    {
      return new TextFieldParser(file);
    }

    /// <summary>The <see langword="OpenTextFieldParser" /> method allows you to create a <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object, which provides a way to easily and efficiently parse structured text files, such as logs. The <see langword="TextFieldParser" /> object can be used to read both delimited and fixed-width files.</summary>
    /// <param name="file">The file to be opened with the <see langword="TextFieldParser" />.</param>
    /// <param name="delimiters">Delimiters for the fields.</param>
    /// <returns>
    /// <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> to read the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:Ported.VisualBasic.FileIO.MalformedLineException">A row cannot be parsed using the specified format. The exception message specifies the line causing the exception, while the <see cref="P:Ported.VisualBasic.FileIO.TextFieldParser.ErrorLine" />  property is assigned the text contained in the line.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static TextFieldParser OpenTextFieldParser(string file, params string[] delimiters)
    {
      TextFieldParser textFieldParser = new TextFieldParser(file);
      textFieldParser.SetDelimiters(delimiters);
      textFieldParser.TextFieldType = FieldType.Delimited;
      return textFieldParser;
    }

    /// <summary>The <see langword="OpenTextFieldParser" /> method allows you to create a <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object, which provides a way to easily and efficiently parse structured text files, such as logs. The <see langword="TextFieldParser" /> object can be used to read both delimited and fixed-width files.</summary>
    /// <param name="file">The file to be opened with the <see langword="TextFieldParser" />.</param>
    /// <param name="fieldWidths">Widths of the fields.</param>
    /// <returns>
    /// <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> to read the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:Ported.VisualBasic.FileIO.MalformedLineException">A row cannot be parsed using the specified format. The exception message specifies the line causing the exception, while the <see cref="P:Ported.VisualBasic.FileIO.TextFieldParser.ErrorLine" />  property is assigned the text contained in the line.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static TextFieldParser OpenTextFieldParser(string file, params int[] fieldWidths)
    {
      TextFieldParser textFieldParser = new TextFieldParser(file);
      textFieldParser.SetFieldWidths(fieldWidths);
      textFieldParser.TextFieldType = FieldType.FixedWidth;
      return textFieldParser;
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamReader" /> object to read from a file.</summary>
    /// <param name="file">File to be read. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamReader" /> object to read from the file</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a backslash (\).</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to read from the file.</exception>
    public static StreamReader OpenTextFileReader(string file)
    {
      return FileSystem.OpenTextFileReader(file, Encoding.UTF8);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamReader" /> object to read from a file.</summary>
    /// <param name="file">File to be read. </param>
    /// <param name="encoding">The encoding to use for the file contents. Default is ASCII. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamReader" /> object to read from the file</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a backslash (\).</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to read from the file.</exception>
    public static StreamReader OpenTextFileReader(string file, Encoding encoding)
    {
      file = FileSystem.NormalizeFilePath(file, nameof (file));
      return new StreamReader(file, encoding, true);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamWriter" /> object to write to the specified file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents of the file; <see langword="False" /> to overwrite the contents of the file. Default is <see langword="False" />. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamWriter" /> object to write to the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a trailing slash.</exception>
    public static StreamWriter OpenTextFileWriter(string file, bool append)
    {
      return FileSystem.OpenTextFileWriter(file, append, Encoding.UTF8);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamWriter" /> to write to the specified file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents in the file; <see langword="False" /> to overwrite the contents of the file. Default is <see langword="False" />. </param>
    /// <param name="encoding">Encoding to be used in writing to the file. Default is ASCII. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamWriter" /> object to write to the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a trailing slash.</exception>
    public static StreamWriter OpenTextFileWriter(string file, bool append, Encoding encoding)
    {
      file = FileSystem.NormalizeFilePath(file, nameof (file));
      return new StreamWriter(file, append, encoding);
    }

    /// <summary>Returns the contents of a file as a byte array.</summary>
    /// <param name="file">File to be read. </param>
    /// <returns>
    /// <see langword="Byte" /> array containing the contents of the file.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static byte[] ReadAllBytes(string file)
    {
      return File.ReadAllBytes(file);
    }

    /// <summary>Returns the contents of a text file as a <see langword="String" />.</summary>
    /// <param name="file">Name and path of the file to read. </param>
    /// <returns>
    /// <see langword="String" /> containing the contents of the file.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static string ReadAllText(string file)
    {
      return File.ReadAllText(file);
    }

    /// <summary>Returns the contents of a text file as a <see langword="String" />.</summary>
    /// <param name="file">Name and path of the file to read. </param>
    /// <param name="encoding">Character encoding to use in reading the file. Default is UTF-8.</param>
    /// <returns>
    /// <see langword="String" /> containing the contents of the file.</returns>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static string ReadAllText(string file, Encoding encoding)
    {
      return File.ReadAllText(file, encoding);
    }

    /// <summary>Copies the contents of a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory contents should be copied.</param>
    /// <exception cref="T:System.ArgumentException">The new name specified for the directory contains a colon (:) or slash (\ or /).</exception>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationDirectoryName" /> or <paramref name="sourceDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The source directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source directory is a root directory</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing file.</exception>
    /// <exception cref="T:System.IO.IOException">The source path and target path are the same.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A folder name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">A destination file exists but cannot be accessed.</exception>

    /*
    public static void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Copy, sourceDirectoryName, destinationDirectoryName, false, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Copies the contents of a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory contents should be copied.</param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />.</param>
    /// <exception cref="T:System.ArgumentException">The new name specified for the directory contains a colon (:) or slash (\ or /).</exception>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationDirectoryName" /> or <paramref name="sourceDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The source directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source directory is a root directory</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing file.</exception>
    /// <exception cref="T:System.IO.IOException">The source path and target path are the same.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A folder name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">A destination file exists but cannot be accessed.</exception>
    public static void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwrite)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Copy, sourceDirectoryName, destinationDirectoryName, overwrite, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Copies the contents of a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory contents should be copied.</param>
    /// <param name="showUI">Whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />.</param>
    /// <exception cref="T:System.ArgumentException">The new name specified for the directory contains a colon (:) or slash (\ or /).</exception>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationDirectoryName" /> or <paramref name="sourceDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The source directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source directory is a root directory</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing file.</exception>
    /// <exception cref="T:System.IO.IOException">The source path and target path are the same.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A folder name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">A destination file exists but cannot be accessed.</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="ShowUI" /> is set to <see langword="UIOption.AllDialogs" /> and the user cancels the operation, or one or more files in the directory cannot be copied.</exception>
    public static void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Copy, sourceDirectoryName, destinationDirectoryName, false, FileSystem.ToUIOptionInternal(showUI), UICancelOption.ThrowException);
    }

    /// <summary>Copies the contents of a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory contents should be copied.</param>
    /// <param name="showUI">Whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />.</param>
    /// <param name="onUserCancel">Specifies what should be done if the user clicks Cancel during the operation. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />.</param>
    /// <exception cref="T:System.ArgumentException">The new name specified for the directory contains a colon (:) or slash (\ or /).</exception>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationDirectoryName" /> or <paramref name="sourceDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The source directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source directory is a root directory</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing file.</exception>
    /// <exception cref="T:System.IO.IOException">The source path and target path are the same.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A folder name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">A destination file exists but cannot be accessed.</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="ShowUI" /> is set to <see langword="UIOption.AllDialogs" /> and the user cancels the operation, or one or more files in the directory cannot be copied.</exception>
    public static void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI, UICancelOption onUserCancel)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Copy, sourceDirectoryName, destinationDirectoryName, false, FileSystem.ToUIOptionInternal(showUI), onUserCancel);
    }

    /// <summary>Copies a file to a new location.</summary>
    /// <param name="sourceFileName">The file to be copied. </param>
    /// <param name="destinationFileName">The location to which the file should be copied. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">The system could not retrieve the absolute path.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName " />contains path information.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> or <paramref name="sourceFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing directory.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have sufficient permissions to access the file.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the target directory with the same name is in use.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void CopyFile(string sourceFileName, string destinationFileName)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Copy, sourceFileName, destinationFileName, false, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Copies a file to a new location.</summary>
    /// <param name="sourceFileName">The file to be copied. </param>
    /// <param name="destinationFileName">The location to which the file should be copied. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> if existing files should be overwritten; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">The system could not retrieve the absolute path.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName " />contains path information.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> or <paramref name="sourceFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing directory.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have sufficient permissions to access the file.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the target directory with the same name is in use.</exception>
    /// <exception cref="T:System.IO.IOException">The destination file exists and <paramref name="overwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Copy, sourceFileName, destinationFileName, overwrite, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Copies a file to a new location.</summary>
    /// <param name="sourceFileName">The file to be copied. </param>
    /// <param name="destinationFileName">The location to which the file should be copied. </param>
    /// <param name="showUI">Whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />.</param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">The system could not retrieve the absolute path.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName " />contains path information.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> or <paramref name="sourceFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing directory.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have sufficient permissions to access the file.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the target directory with the same name is in use.</exception>
    /// <exception cref="T:System.IO.IOException">The destination file exists and <paramref name="overwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void CopyFile(string sourceFileName, string destinationFileName, UIOption showUI)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Copy, sourceFileName, destinationFileName, false, FileSystem.ToUIOptionInternal(showUI), UICancelOption.ThrowException);
    }

    /// <summary>Copies a file to a new location.</summary>
    /// <param name="sourceFileName">The file to be copied. </param>
    /// <param name="destinationFileName">The location to which the file should be copied. </param>
    /// <param name="showUI">Whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />.</param>
    /// <param name="onUserCancel">Specifies what should be done if the user clicks Cancel during the operation. Default is <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">The system could not retrieve the absolute path.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="destinationFileName " />contains path information.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> or <paramref name="sourceFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The combined path points to an existing directory.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have sufficient permissions to access the file.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the target directory with the same name is in use.</exception>
    /// <exception cref="T:System.IO.IOException">The destination file exists and <paramref name="overwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="UICancelOption" /> is set to <see langword="ThrowException" />, and the user has canceled the operation or an unspecified I/O error occurs.</exception>
    public static void CopyFile(string sourceFileName, string destinationFileName, UIOption showUI, UICancelOption onUserCancel)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Copy, sourceFileName, destinationFileName, false, FileSystem.ToUIOptionInternal(showUI), onUserCancel);
    }

    /// <summary>Creates a directory.</summary>
    /// <param name="directory">Name and location of the directory. </param>
    /// <exception cref="T:System.ArgumentException">The directory name is malformed. For example, it contains illegal characters or is only white space.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The directory name is too long.</exception>
    /// <exception cref="T:System.NotSupportedException">The directory name is only a colon (:).</exception>
    /// <exception cref="T:System.IO.IOException">The parent directory of the directory to be created is read-only</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have permission to create the directory.</exception>
    public static void CreateDirectory(string directory)
    {
      directory = Path.GetFullPath(directory);
      if (File.Exists(directory))
        throw ExceptionUtils.GetIOException("IO_FileExists_Path", directory);
      Directory.CreateDirectory(directory);
    }

    /// <summary>Deletes a directory.</summary>
    /// <param name="directory">Directory to be deleted. </param>
    /// <param name="onDirectoryNotEmpty">Specifies what should be done when a directory that is to be deleted contains files or directories. Default is <see langword="DeleteDirectoryOption.DeleteAllContents" />.</param>
    /// <exception cref="T:System.ArgumentException">The path is a zero-length string, is malformed, contains only white space, or contains invalid characters (including wildcard characters). The path is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist or is a file.</exception>
    /// <exception cref="T:System.IO.IOException">The directory is not empty, and <paramref name="onDirectoryNotEmpty" /> is set to <see langword="ThrowIfDirectoryNonEmpty" />.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have permission to delete the directory or subdirectory.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the directory or subdirectory is in use.</exception>
    /// <exception cref="T:System.NotSupportedException">The directory name contains a colon (:).</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user does not have required permissions.</exception>
    /// <exception cref="T:System.OperationCanceledException">The user cancels the operation or the directory cannot be deleted.</exception>
    public static void DeleteDirectory(string directory, DeleteDirectoryOption onDirectoryNotEmpty)
    {
      FileSystem.DeleteDirectoryInternal(directory, onDirectoryNotEmpty, FileSystem.UIOptionInternal.NoUI, RecycleOption.DeletePermanently, UICancelOption.ThrowException);
    }

    /// <summary>Deletes a directory.</summary>
    /// <param name="directory">Directory to be deleted. </param>
    /// <param name="showUI">Specifies whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <param name="recycle">Specifies whether or not the deleted file should be sent to the Recycle Bin. Default is <see langword="RecycleOption.DeletePermanently" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is a zero-length string, is malformed, contains only white space, or contains invalid characters (including wildcard characters). The path is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist or is a file.</exception>
    /// <exception cref="T:System.IO.IOException">The directory is not empty, and <paramref name="onDirectoryNotEmpty" /> is set to <see langword="ThrowIfDirectoryNonEmpty" />.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have permission to delete the directory or subdirectory.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the directory or subdirectory is in use.</exception>
    /// <exception cref="T:System.NotSupportedException">The directory name contains a colon (:).</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user does not have required permissions.</exception>
    /// <exception cref="T:System.OperationCanceledException">The user cancels the operation or the directory cannot be deleted.</exception>
    public static void DeleteDirectory(string directory, UIOption showUI, RecycleOption recycle)
    {
      FileSystem.DeleteDirectoryInternal(directory, DeleteDirectoryOption.DeleteAllContents, FileSystem.ToUIOptionInternal(showUI), recycle, UICancelOption.ThrowException);
    }

    /// <summary>Deletes a directory.</summary>
    /// <param name="directory">Directory to be deleted. </param>
    /// <param name="showUI">Specifies whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <param name="recycle">Specifies whether or not the deleted file should be sent to the Recycle Bin. Default is <see langword="RecycleOption.DeletePermanently" />. </param>
    /// <param name="onUserCancel">Specifies whether to throw an exception if the user clicks Cancel. </param>
    /// <exception cref="T:System.ArgumentException">The path is a zero-length string, is malformed, contains only white space, or contains invalid characters (including wildcard characters). The path is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist or is a file.</exception>
    /// <exception cref="T:System.IO.IOException">The directory is not empty, and <paramref name="onDirectoryNotEmpty" /> is set to <see langword="ThrowIfDirectoryNonEmpty" />.</exception>
    /// <exception cref="T:System.IO.IOException">The user does not have permission to delete the directory or subdirectory.</exception>
    /// <exception cref="T:System.IO.IOException">A file in the directory or subdirectory is in use.</exception>
    /// <exception cref="T:System.NotSupportedException">The directory name contains a colon (:).</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user does not have required permissions.</exception>
    /// <exception cref="T:System.OperationCanceledException">The user cancels the operation or the directory cannot be deleted.</exception>
    public static void DeleteDirectory(string directory, UIOption showUI, RecycleOption recycle, UICancelOption onUserCancel)
    {
      FileSystem.DeleteDirectoryInternal(directory, DeleteDirectoryOption.DeleteAllContents, FileSystem.ToUIOptionInternal(showUI), recycle, onUserCancel);
    }

    /// <summary>Deletes a file.</summary>
    /// <param name="file">Name and path of the file to be deleted. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; it has a trailing slash where a file must be specified; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have permission to delete the file or the file is read-only.</exception>
    public static void DeleteFile(string file)
    {
      FileSystem.DeleteFileInternal(file, FileSystem.UIOptionInternal.NoUI, RecycleOption.DeletePermanently, UICancelOption.ThrowException);
    }

    /// <summary>Deletes a file.</summary>
    /// <param name="file">Name and path of the file to be deleted. </param>
    /// <param name="showUI">Whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <param name="recycle">Whether or not the deleted file should be sent to the Recycle Bin. Default is <see langword="RecycleOption.DeletePermanently" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; it has a trailing slash where a file must be specified; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have permission to delete the file or the file is read-only.</exception>
    public static void DeleteFile(string file, UIOption showUI, RecycleOption recycle)
    {
      FileSystem.DeleteFileInternal(file, FileSystem.ToUIOptionInternal(showUI), recycle, UICancelOption.ThrowException);
    }

    /// <summary>Deletes a file.</summary>
    /// <param name="file">Name and path of the file to be deleted. </param>
    /// <param name="showUI">Whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <param name="recycle">Whether or not the deleted file should be sent to the Recycle Bin. Default is <see langword="RecycleOption.DeletePermanently" />. </param>
    /// <param name="onUserCancel">Specifies whether or not an exception is thrown when the user cancels the operation. Default is <see langword="UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; it has a trailing slash where a file must be specified; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have permission to delete the file or the file is read-only.</exception>
    /// <exception cref="T:System.OperationCanceledException">The user cancelled the operation and <paramref name="onUserCancel" /> is set to <see cref="F:Ported.VisualBasic.FileIO.UICancelOption.ThrowException" />.</exception>
    public static void DeleteFile(string file, UIOption showUI, RecycleOption recycle, UICancelOption onUserCancel)
    {
      FileSystem.DeleteFileInternal(file, FileSystem.ToUIOptionInternal(showUI), recycle, onUserCancel);
    }

    /// <summary>Moves a directory from one location to another.</summary>
    /// <param name="sourceDirectoryName">Path of the directory to be moved. </param>
    /// <param name="destinationDirectoryName">Path of the directory to which the source directory is being moved. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source is a root directory or The source path and the target path are the same.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public static void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Move, sourceDirectoryName, destinationDirectoryName, false, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Moves a directory from one location to another.</summary>
    /// <param name="sourceDirectoryName">Path of the directory to be moved. </param>
    /// <param name="destinationDirectoryName">Path of the directory to which the source directory is being moved. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> if existing directories should be overwritten; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source is a root directory or The source path and the target path are the same.</exception>
    /// <exception cref="T:System.IO.IOException">The target directory already exists and o<paramref name="verwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public static void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwrite)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Move, sourceDirectoryName, destinationDirectoryName, overwrite, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Moves a directory from one location to another.</summary>
    /// <param name="sourceDirectoryName">Path of the directory to be moved. </param>
    /// <param name="destinationDirectoryName">Path of the directory to which the source directory is being moved. </param>
    /// <param name="showUI">Specifies whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source is a root directory or The source path and the target path are the same.</exception>
    /// <exception cref="T:System.IO.IOException">The target directory already exists and o<paramref name="verwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public static void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Move, sourceDirectoryName, destinationDirectoryName, false, FileSystem.ToUIOptionInternal(showUI), UICancelOption.ThrowException);
    }

    /// <summary>Moves a directory from one location to another.</summary>
    /// <param name="sourceDirectoryName">Path of the directory to be moved. </param>
    /// <param name="destinationDirectoryName">Path of the directory to which the source directory is being moved. </param>
    /// <param name="showUI">Specifies whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <param name="onUserCancel">Specifies whether or not an exception is thrown when the user cancels the operation. Default is <see langword="UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="sourceDirectoryName" /> or <paramref name="destinationDirectoryName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The source is a root directory or The source path and the target path are the same.</exception>
    /// <exception cref="T:System.IO.IOException">The target directory already exists and o<paramref name="verwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.IO.IOException">
    /// <paramref name="onUserCancel" /> is set to <see langword="ThrowException" /> and a subdirectory of the file cannot be copied.</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="onUserCancel" /> is set to <see langword="ThrowException" />, and the user cancels the operation, or the operation cannot be completed.</exception>
    /// <exception cref="T:System.Security.SecurityException">
    /// <paramref name="onUserCancel" /> is set to <see langword="ThrowException" />, and the user lacks necessary permissions.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.InvalidOperationException">The operation is cyclic.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public static void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI, UICancelOption onUserCancel)
    {
      FileSystem.CopyOrMoveDirectory(FileSystem.CopyOrMove.Move, sourceDirectoryName, destinationDirectoryName, false, FileSystem.ToUIOptionInternal(showUI), onUserCancel);
    }

    /// <summary>Moves a file to a new location.</summary>
    /// <param name="sourceFileName">Path of the file to be moved. </param>
    /// <param name="destinationFileName">Path of the directory into which the file should be moved. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void MoveFile(string sourceFileName, string destinationFileName)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Move, sourceFileName, destinationFileName, false, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Moves a file to a new location.</summary>
    /// <param name="sourceFileName">Path of the file to be moved. </param>
    /// <param name="destinationFileName">Path of the directory into which the file should be moved. </param>
    /// <param name="overwrite">
    /// <see langword="True" /> to overwrite existing files; otherwise <see langword="False" />. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The destination file exists and <paramref name="overwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void MoveFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Move, sourceFileName, destinationFileName, overwrite, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
    }

    /// <summary>Moves a file to a new location.</summary>
    /// <param name="sourceFileName">Path of the file to be moved. </param>
    /// <param name="destinationFileName">Path of the directory into which the file should be moved. </param>
    /// <param name="showUI">Specifies whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The destination file exists and <paramref name="overwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void MoveFile(string sourceFileName, string destinationFileName, UIOption showUI)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Move, sourceFileName, destinationFileName, false, FileSystem.ToUIOptionInternal(showUI), UICancelOption.ThrowException);
    }

    /// <summary>Moves a file to a new location.</summary>
    /// <param name="sourceFileName">Path of the file to be moved. </param>
    /// <param name="destinationFileName">Path of the directory into which the file should be moved. </param>
    /// <param name="showUI">Specifies whether to visually track the operation's progress. Default is <see langword="UIOption.OnlyErrorDialogs" />. </param>
    /// <param name="onUserCancel">Specifies whether or not an exception is thrown when the user cancels the operation. Default is <see langword="UICancelOption.ThrowException" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="destinationFileName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The source file is not valid or does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The destination file exists and <paramref name="overwrite" /> is set to <see langword="False" />.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.OperationCanceledException">
    /// <paramref name="onUserCancel" /> is set to <see langword="ThrowException" />, and either the user has cancelled the operation or an unspecified I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void MoveFile(string sourceFileName, string destinationFileName, UIOption showUI, UICancelOption onUserCancel)
    {
      FileSystem.CopyOrMoveFile(FileSystem.CopyOrMove.Move, sourceFileName, destinationFileName, false, FileSystem.ToUIOptionInternal(showUI), onUserCancel);
    }

    /// <summary>Renames a directory.</summary>
    /// <param name="directory">Path and name of directory to be renamed. </param>
    /// <param name="newName">New name for directory. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="newName" /> contains path information.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="directory" /> is <see langword="Nothing" />.-or-
    /// <paramref name="newName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">There is an existing file or directory with the name specified in <paramref name="newName" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds 248 characters.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public static void RenameDirectory(string directory, string newName)
    {
      directory = Path.GetFullPath(directory);
      FileSystem.ThrowIfDevicePath(directory);
      if (FileSystem.IsRoot(directory))
        throw ExceptionUtils.GetIOException("IO_DirectoryIsRoot_Path", directory);
      if (!Directory.Exists(directory))
        throw ExceptionUtils.GetDirectoryNotFoundException("IO_DirectoryNotFound_Path", directory);
      if (Operators.CompareString(newName, "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (newName), "General_ArgumentEmptyOrNothing_Name", nameof (newName));
      string fullPathFromNewName = FileSystem.GetFullPathFromNewName(FileSystem.GetParentPath(directory), newName, nameof (newName));
      FileSystem.EnsurePathNotExist(fullPathFromNewName);
      Directory.Move(directory, fullPathFromNewName);
    }
*/
    /// <summary>Renames a file.</summary>
    /// <param name="file">File to be renamed. </param>
    /// <param name="newName">New name of file. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="newName" /> contains path information or ends with a backslash (\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.-or-
    /// <paramref name="newName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The directory does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">There is an existing file or directory with the name specified in <paramref name="newName" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public static void RenameFile(string file, string newName)
    {
      file = FileSystem.NormalizeFilePath(file, nameof (file));
      FileSystem.ThrowIfDevicePath(file);
      if (!File.Exists(file))
        throw ExceptionUtils.GetFileNotFoundException(file, "IO_FileNotFound_Path", file);
      if (Operators.CompareString(newName, "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (newName), "General_ArgumentEmptyOrNothing_Name", nameof (newName));
      string fullPathFromNewName = FileSystem.GetFullPathFromNewName(FileSystem.GetParentPath(file), newName, nameof (newName));
      FileSystem.EnsurePathNotExist(fullPathFromNewName);
      File.Move(file, fullPathFromNewName);
    }

    /// <summary>Writes data to a binary file.</summary>
    /// <param name="file">Path and name of the file to be written to. </param>
    /// <param name="data">Data to be written to the file. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the file contents; <see langword="False" /> to overwrite the file contents. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void WriteAllBytes(string file, byte[] data, bool append)
    {
      FileSystem.CheckFilePathTrailingSeparator(file, nameof (file));
      FileStream fileStream = (FileStream) null;
      try
      {
        FileMode mode = !append ? FileMode.Create : FileMode.Append;
        fileStream = new FileStream(file, mode, FileAccess.Write, FileShare.Read);
        fileStream.Write(data, 0, data.Length);
      }
      finally
      {
        fileStream?.Close();
      }
    }

    /// <summary>Writes text to a file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="text">Text to be written to file. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents of the file; <see langword="False" /> to overwrite the contents of the file. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void WriteAllText(string file, string text, bool append)
    {
      FileSystem.WriteAllText(file, text, append, Encoding.UTF8);
    }

    /// <summary>Writes text to a file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="text">Text to be written to file. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents of the file; <see langword="False" /> to overwrite the contents of the file. </param>
    /// <param name="encoding">What encoding to use when writing to file. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public static void WriteAllText(string file, string text, bool append, Encoding encoding)
    {
      FileSystem.CheckFilePathTrailingSeparator(file, nameof (file));
      StreamWriter streamWriter = (StreamWriter) null;
      try
      {
        if (append && File.Exists(file))
        {
          StreamReader streamReader = (StreamReader) null;
          try
          {
            streamReader = new StreamReader(file, encoding, true);
            char[] buffer = new char[10];
            streamReader.Read(buffer, 0, 10);
            encoding = streamReader.CurrentEncoding;
          }
          catch (IOException ex)
          {
          }
          finally
          {
            streamReader?.Close();
          }
        }
        streamWriter = new StreamWriter(file, append, encoding);
        streamWriter.Write(text);
      }
      finally
      {
        streamWriter?.Close();
      }
    }

    internal static string NormalizeFilePath(string Path, string ParamName)
    {
      FileSystem.CheckFilePathTrailingSeparator(Path, ParamName);
      return FileSystem.NormalizePath(Path);
    }

    internal static string NormalizePath(string Path)
    {
      return FileSystem.GetLongPath(FileSystem.RemoveEndingSeparator(System.IO.Path.GetFullPath(Path)));
    }

    internal static void CheckFilePathTrailingSeparator(string path, string paramName)
    {
      if (Operators.CompareString(path, "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(paramName);
      if (path.EndsWith(Conversions.ToString(Path.DirectorySeparatorChar), StringComparison.Ordinal) | path.EndsWith(Conversions.ToString(Path.AltDirectorySeparatorChar), StringComparison.Ordinal))
        throw ExceptionUtils.GetArgumentExceptionWithArgName(paramName, "IO_FilePathException");
    }

    private static void AddToStringCollection(Collection<string> StrCollection, string[] StrArray)
    {
      if (StrArray == null)
        return;
      string[] strArray = StrArray;
      int index = 0;
      while (index < strArray.Length)
      {
        string str = strArray[index];
        if (!StrCollection.Contains(str))
          StrCollection.Add(str);
        checked { ++index; }
      }
    }
/*
    private static void CopyOrMoveDirectory(FileSystem.CopyOrMove operation, string sourceDirectoryName, string destinationDirectoryName, bool overwrite, FileSystem.UIOptionInternal showUI, UICancelOption onUserCancel)
    {
      FileSystem.VerifyUICancelOption(nameof (onUserCancel), onUserCancel);
      string str1 = FileSystem.NormalizePath(sourceDirectoryName);
      string str2 = FileSystem.NormalizePath(destinationDirectoryName);
      FileIOPermissionAccess access = FileIOPermissionAccess.Read;
      if (operation == FileSystem.CopyOrMove.Move)
        access |= FileIOPermissionAccess.Write;
      FileSystem.DemandDirectoryPermission(str1, access);
      FileSystem.DemandDirectoryPermission(str2, FileIOPermissionAccess.Read | FileIOPermissionAccess.Write);
      FileSystem.ThrowIfDevicePath(str1);
      FileSystem.ThrowIfDevicePath(str2);
      if (!Directory.Exists(str1))
        throw ExceptionUtils.GetDirectoryNotFoundException("IO_DirectoryNotFound_Path", sourceDirectoryName);
      if (FileSystem.IsRoot(str1))
        throw ExceptionUtils.GetIOException("IO_DirectoryIsRoot_Path", sourceDirectoryName);
      if (File.Exists(str2))
        throw ExceptionUtils.GetIOException("IO_FileExists_Path", destinationDirectoryName);
      if (str2.Equals(str1, StringComparison.OrdinalIgnoreCase))
        throw ExceptionUtils.GetIOException("IO_SourceEqualsTargetDirectory");
      if (str2.Length > str1.Length && str2.Substring(0, str1.Length).Equals(str1, StringComparison.OrdinalIgnoreCase) && (int) str2[str1.Length] == (int) Path.DirectorySeparatorChar)
        throw ExceptionUtils.GetInvalidOperationException("IO_CyclicOperation");
      if (showUI != FileSystem.UIOptionInternal.NoUI && Environment.UserInteractive)
        FileSystem.ShellCopyOrMove(operation, FileSystem.FileOrDirectory.Directory, str1, str2, showUI, onUserCancel);
      else
        FileSystem.FxCopyOrMoveDirectory(operation, str1, str2, overwrite);
    }

    private static void FxCopyOrMoveDirectory(FileSystem.CopyOrMove operation, string sourceDirectoryPath, string targetDirectoryPath, bool overwrite)
    {
      if (operation == FileSystem.CopyOrMove.Move & !Directory.Exists(targetDirectoryPath) & FileSystem.IsOnSameDrive(sourceDirectoryPath, targetDirectoryPath))
      {
        Directory.CreateDirectory(FileSystem.GetParentPath(targetDirectoryPath));
        try
        {
          Directory.Move(sourceDirectoryPath, targetDirectoryPath);
          return;
        }
        catch (IOException ex)
        {
        }
        catch (UnauthorizedAccessException ex)
        {
        }
      }
      Directory.CreateDirectory(targetDirectoryPath);
      FileSystem.DirectoryNode SourceDirectoryNode = new FileSystem.DirectoryNode(sourceDirectoryPath, targetDirectoryPath);
      ListDictionary Exceptions = new ListDictionary();
      FileSystem.CopyOrMoveDirectoryNode(operation, SourceDirectoryNode, overwrite, Exceptions);
      if (Exceptions.Count > 0)
      {
        IOException ioException = new IOException(Utils.GetResourceString("IO_CopyMoveRecursive"));
        foreach (object obj in Exceptions)
        {
          DictionaryEntry dictionaryEntry1;
          DictionaryEntry dictionaryEntry2 = obj != null ? (DictionaryEntry) obj : dictionaryEntry1;
          ioException.Data.Add(dictionaryEntry2.Key, dictionaryEntry2.Value);
        }
        throw ioException;
      }
    }

    private static void CopyOrMoveDirectoryNode(FileSystem.CopyOrMove Operation, FileSystem.DirectoryNode SourceDirectoryNode, bool Overwrite, ListDictionary Exceptions)
    {
      try
      {
        if (!Directory.Exists(SourceDirectoryNode.TargetPath))
          Directory.CreateDirectory(SourceDirectoryNode.TargetPath);
      }
      catch (Exception ex)
      {
        if (ex is IOException || ex is UnauthorizedAccessException || (ex is DirectoryNotFoundException || ex is NotSupportedException) || ex is SecurityException)
        {
          Exceptions.Add((object) SourceDirectoryNode.Path, (object) ex.Message);
          return;
        }
        throw;
      }
      if (!Directory.Exists(SourceDirectoryNode.TargetPath))
      {
        Exceptions.Add((object) SourceDirectoryNode.TargetPath, (object) ExceptionUtils.GetDirectoryNotFoundException("IO_DirectoryNotFound_Path", SourceDirectoryNode.TargetPath));
      }
      else
      {
        string[] files = Directory.GetFiles(SourceDirectoryNode.Path);
        int index = 0;
        while (index < files.Length)
        {
          string str = files[index];
          try
          {
            FileSystem.CopyOrMoveFile(Operation, str, Path.Combine(SourceDirectoryNode.TargetPath, Path.GetFileName(str)), Overwrite, FileSystem.UIOptionInternal.NoUI, UICancelOption.ThrowException);
          }
          catch (Exception ex)
          {
            if (ex is IOException || ex is UnauthorizedAccessException || (ex is SecurityException || ex is NotSupportedException))
              Exceptions.Add((object) str, (object) ex.Message);
            else
              throw;
          }
          checked { ++index; }
        }
        IEnumerator<FileSystem.DirectoryNode> enumerator=Ported.VisualBasic.CompilerServices.OverloadResolution.InitEnumerator<FileSystem.DirectoryNode>();
        try
        {
          enumerator = SourceDirectoryNode.SubDirs.GetEnumerator();
          while (enumerator.MoveNext())
          {
            FileSystem.DirectoryNode current = enumerator.Current;
            FileSystem.CopyOrMoveDirectoryNode(Operation, current, Overwrite, Exceptions);
          }
        }
        finally
        {
          enumerator?.Dispose();
        }
        if (Operation != FileSystem.CopyOrMove.Move)
          return;
        try
        {
          Directory.Delete(SourceDirectoryNode.Path, false);
        }
        catch (Exception ex)
        {
          if (ex is IOException || ex is UnauthorizedAccessException || (ex is SecurityException || ex is DirectoryNotFoundException))
            Exceptions.Add((object) SourceDirectoryNode.Path, (object) ex.Message);
          else
            throw;
        }
      }
    }
    private static void CopyOrMoveFile(FileSystem.CopyOrMove operation, string sourceFileName, string destinationFileName, bool overwrite, FileSystem.UIOptionInternal showUI, UICancelOption onUserCancel)
    {
      FileSystem.VerifyUICancelOption(nameof (onUserCancel), onUserCancel);
      string str1 = FileSystem.NormalizeFilePath(sourceFileName, nameof (sourceFileName));
      string str2 = FileSystem.NormalizeFilePath(destinationFileName, nameof (destinationFileName));
      FileIOPermissionAccess access = FileIOPermissionAccess.Read;
      if (operation == FileSystem.CopyOrMove.Move)
        access |= FileIOPermissionAccess.Write;
      new FileIOPermission(access, str1).Demand();
      new FileIOPermission(FileIOPermissionAccess.Write, str2).Demand();
      FileSystem.ThrowIfDevicePath(str1);
      FileSystem.ThrowIfDevicePath(str2);
      if (!File.Exists(str1))
        throw ExceptionUtils.GetFileNotFoundException(sourceFileName, "IO_FileNotFound_Path", sourceFileName);
      if (Directory.Exists(str2))
        throw ExceptionUtils.GetIOException("IO_DirectoryExists_Path", destinationFileName);
      Directory.CreateDirectory(FileSystem.GetParentPath(str2));
      if (showUI != FileSystem.UIOptionInternal.NoUI && Environment.UserInteractive)
        FileSystem.ShellCopyOrMove(operation, FileSystem.FileOrDirectory.File, str1, str2, showUI, onUserCancel);
      else if (operation == FileSystem.CopyOrMove.Copy || str1.Equals(str2, StringComparison.OrdinalIgnoreCase))
        File.Copy(str1, str2, overwrite);
      else if (overwrite)
      {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
          new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
          try
          {
            if (NativeMethods.MoveFileEx(str1, str2, 11))
              return;
            FileSystem.ThrowWinIOError(Marshal.GetLastWin32Error());
          }
          catch (Exception ex)
          {
            throw;
          }
          finally
          {
            CodeAccessPermission.RevertAssert();
          }
        }
        else
        {
          File.Delete(str2);
          File.Move(str1, str2);
        }
      }
      else
        File.Move(str1, str2);
    }

    private static void DeleteDirectoryInternal(string directory, DeleteDirectoryOption onDirectoryNotEmpty, FileSystem.UIOptionInternal showUI, RecycleOption recycle, UICancelOption onUserCancel)
    {
      FileSystem.VerifyDeleteDirectoryOption(nameof (onDirectoryNotEmpty), onDirectoryNotEmpty);
      FileSystem.VerifyRecycleOption(nameof (recycle), recycle);
      FileSystem.VerifyUICancelOption(nameof (onUserCancel), onUserCancel);
      string fullPath = Path.GetFullPath(directory);
      FileSystem.DemandDirectoryPermission(fullPath, FileIOPermissionAccess.Write);
      FileSystem.ThrowIfDevicePath(fullPath);
      if (!Directory.Exists(fullPath))
        throw ExceptionUtils.GetDirectoryNotFoundException("IO_DirectoryNotFound_Path", directory);
      if (FileSystem.IsRoot(fullPath))
        throw ExceptionUtils.GetIOException("IO_DirectoryIsRoot_Path", directory);
      if (showUI != FileSystem.UIOptionInternal.NoUI && Environment.UserInteractive)
        FileSystem.ShellDelete(fullPath, showUI, recycle, onUserCancel, FileSystem.FileOrDirectory.Directory);
      else
        Directory.Delete(fullPath, onDirectoryNotEmpty == DeleteDirectoryOption.DeleteAllContents);
    }

    private static void DeleteFileInternal(string file, FileSystem.UIOptionInternal showUI, RecycleOption recycle, UICancelOption onUserCancel)
    {
      FileSystem.VerifyRecycleOption(nameof (recycle), recycle);
      FileSystem.VerifyUICancelOption(nameof (onUserCancel), onUserCancel);
      string str = FileSystem.NormalizeFilePath(file, nameof (file));
      new FileIOPermission(FileIOPermissionAccess.Write, str).Demand();
      FileSystem.ThrowIfDevicePath(str);
      if (!File.Exists(str))
        throw ExceptionUtils.GetFileNotFoundException(file, "IO_FileNotFound_Path", file);
      if (showUI != FileSystem.UIOptionInternal.NoUI && Environment.UserInteractive)
        FileSystem.ShellDelete(str, showUI, recycle, onUserCancel, FileSystem.FileOrDirectory.File);
      else
        File.Delete(str);
    }
*/
    /*
    private static void DemandDirectoryPermission(string fullDirectoryPath, FileIOPermissionAccess access)
    {
      if (!(fullDirectoryPath.EndsWith(Conversions.ToString(Path.DirectorySeparatorChar), StringComparison.Ordinal) | fullDirectoryPath.EndsWith(Conversions.ToString(Path.AltDirectorySeparatorChar), StringComparison.Ordinal)))
        fullDirectoryPath += Conversions.ToString(Path.DirectorySeparatorChar);
      new FileIOPermission(access, fullDirectoryPath).Demand();
    }
*/
    private static void EnsurePathNotExist(string Path)
    {
      if (File.Exists(Path))
        throw ExceptionUtils.GetIOException("IO_FileExists_Path", Path);
      if (Directory.Exists(Path))
        throw ExceptionUtils.GetIOException("IO_DirectoryExists_Path", Path);
    }

    private static bool FileContainsText(string FilePath, string Text, bool IgnoreCase)
    {
      int val2 = 1024;
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        Encoding currentEncoding = Encoding.Default;
        byte[] numArray = new byte[checked (val2 - 1 + 1)];
        int num1 = fileStream.Read(numArray, 0, numArray.Length);
        if (num1 > 0)
        {
          StreamReader streamReader = new StreamReader((Stream) new MemoryStream(numArray, 0, num1), currentEncoding, true);
          streamReader.ReadLine();
          currentEncoding = streamReader.CurrentEncoding;
        }
        int num2 = Math.Max(currentEncoding.GetMaxByteCount(Text.Length), val2);
        FileSystem.TextSearchHelper textSearchHelper = new FileSystem.TextSearchHelper(currentEncoding, Text, IgnoreCase);
        if (num2 > val2)
        {
          numArray = (byte[]) Utils.CopyArray((Array) numArray, (Array) new byte[checked (num2 - 1 + 1)]);
          int num3 = fileStream.Read(numArray, num1, checked (numArray.Length - num1));
          checked { num1 += num3; }
        }
        while (num1 <= 0 || !textSearchHelper.IsTextFound(numArray, num1))
        {
          num1 = fileStream.Read(numArray, 0, numArray.Length);
          if (num1 <= 0)
            return false;
        }
        return true;
      }
      catch (Exception ex)
      {
        if (ex is IOException | ex is NotSupportedException | ex is SecurityException | ex is UnauthorizedAccessException)
          return false;
        throw;
      }
      finally
      {
        fileStream?.Close();
      }
    }

    private static ReadOnlyCollection<string> FindFilesOrDirectories(FileSystem.FileOrDirectory FileOrDirectory, string directory, SearchOption searchType, string[] wildcards)
    {
      Collection<string> Results = new Collection<string>();
      FileSystem.FindFilesOrDirectories(FileOrDirectory, directory, searchType, wildcards, Results);
      return new ReadOnlyCollection<string>((IList<string>) Results);
    }

    private static void FindFilesOrDirectories(FileSystem.FileOrDirectory FileOrDirectory, string directory, SearchOption searchType, string[] wildcards, Collection<string> Results)
    {
      FileSystem.VerifySearchOption(nameof (searchType), searchType);
      directory = FileSystem.NormalizePath(directory);
      if (wildcards != null)
      {
        string[] strArray = wildcards;
        int index = 0;
        while (index < strArray.Length)
        {
          if (Operators.CompareString(strArray[index].TrimEnd(), "", false) == 0)
            throw ExceptionUtils.GetArgumentNullException(nameof (wildcards), "IO_GetFiles_NullPattern");
          checked { ++index; }
        }
      }
      if (wildcards == null || wildcards.Length == 0)
      {
        FileSystem.AddToStringCollection(Results, FileSystem.FindPaths(FileOrDirectory, directory, (string) null));
      }
      else
      {
        string[] strArray = wildcards;
        int index = 0;
        while (index < strArray.Length)
        {
          string wildCard = strArray[index];
          FileSystem.AddToStringCollection(Results, FileSystem.FindPaths(FileOrDirectory, directory, wildCard));
          checked { ++index; }
        }
      }
      if (searchType != SearchOption.SearchAllSubDirectories)
        return;
      string[] directories = Directory.GetDirectories(directory);
      int index1 = 0;
      while (index1 < directories.Length)
      {
        string directory1 = directories[index1];
        FileSystem.FindFilesOrDirectories(FileOrDirectory, directory1, searchType, wildcards, Results);
        checked { ++index1; }
      }
    }

    private static string[] FindPaths(FileSystem.FileOrDirectory FileOrDirectory, string directory, string wildCard)
    {
      if (FileOrDirectory == FileSystem.FileOrDirectory.Directory)
      {
        if (Operators.CompareString(wildCard, "", false) == 0)
          return Directory.GetDirectories(directory);
        return Directory.GetDirectories(directory, wildCard);
      }
      if (Operators.CompareString(wildCard, "", false) == 0)
        return Directory.GetFiles(directory);
      return Directory.GetFiles(directory, wildCard);
    }

    private static string GetFullPathFromNewName(string Path, string NewName, string ArgumentName)
    {
      if (NewName.IndexOfAny(FileSystem.m_SeparatorChars) >= 0)
        throw ExceptionUtils.GetArgumentExceptionWithArgName(ArgumentName, "IO_ArgumentIsPath_Name_Path", ArgumentName, NewName);
      string path = FileSystem.RemoveEndingSeparator(System.IO.Path.GetFullPath(System.IO.Path.Combine(Path, NewName)));
      if (!FileSystem.GetParentPath(path).Equals(Path, StringComparison.OrdinalIgnoreCase))
        throw ExceptionUtils.GetArgumentExceptionWithArgName(ArgumentName, "IO_ArgumentIsPath_Name_Path", ArgumentName, NewName);
      return path;
    }

    private static string GetLongPath(string FullPath)
    {
      try
      {
        if (FileSystem.IsRoot(FullPath))
          return FullPath;
        DirectoryInfo directoryInfo = new DirectoryInfo(FileSystem.GetParentPath(FullPath));
        if (File.Exists(FullPath))
          return directoryInfo.GetFiles(Path.GetFileName(FullPath))[0].FullName;
        if (Directory.Exists(FullPath))
          return directoryInfo.GetDirectories(Path.GetFileName(FullPath))[0].FullName;
        return FullPath;
      }
      catch (Exception ex)
      {
        if (ex is ArgumentException || ex is ArgumentNullException || (ex is PathTooLongException || ex is NotSupportedException) || (ex is DirectoryNotFoundException || ex is SecurityException || ex is UnauthorizedAccessException))
          return FullPath;
        throw;
      }
    }

    private static bool IsOnSameDrive(string Path1, string Path2)
    {
      Path1 = Path1.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
      Path2 = Path2.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
      return string.Compare(Path.GetPathRoot(Path1), Path.GetPathRoot(Path2), StringComparison.OrdinalIgnoreCase) == 0;
    }

    private static bool IsRoot(string Path)
    {
      if (!System.IO.Path.IsPathRooted(Path))
        return false;
      Path = Path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
      return string.Compare(Path, System.IO.Path.GetPathRoot(Path), StringComparison.OrdinalIgnoreCase) == 0;
    }

    private static string RemoveEndingSeparator(string Path)
    {
      if (System.IO.Path.IsPathRooted(Path) && Path.Equals(System.IO.Path.GetPathRoot(Path), StringComparison.OrdinalIgnoreCase))
        return Path;
      return Path.TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
    }

    
    /*
    private static void ShellCopyOrMove(FileSystem.CopyOrMove Operation, FileSystem.FileOrDirectory TargetType, string FullSourcePath, string FullTargetPath, FileSystem.UIOptionInternal ShowUI, UICancelOption OnUserCancel)
    {
      NativeMethods.SHFileOperationType OperationType = Operation != FileSystem.CopyOrMove.Copy ? NativeMethods.SHFileOperationType.FO_MOVE : NativeMethods.SHFileOperationType.FO_COPY;
      NativeMethods.ShFileOperationFlags operationFlags = FileSystem.GetOperationFlags(ShowUI);
      string FullSource = FullSourcePath;
      if (TargetType == FileSystem.FileOrDirectory.Directory)
      {
        if (Directory.Exists(FullTargetPath))
          FullSource = Path.Combine(FullSourcePath, "*");
        else
          Directory.CreateDirectory(FileSystem.GetParentPath(FullTargetPath));
      }
      FileSystem.ShellFileOperation(OperationType, operationFlags, FullSource, FullTargetPath, OnUserCancel, TargetType);
      if (!(Operation == FileSystem.CopyOrMove.Move & TargetType == FileSystem.FileOrDirectory.Directory) || !Directory.Exists(FullSourcePath) || (Directory.GetDirectories(FullSourcePath).Length != 0 || Directory.GetFiles(FullSourcePath).Length != 0))
        return;
      Directory.Delete(FullSourcePath, false);
    }

    private static void ShellDelete(string FullPath, FileSystem.UIOptionInternal ShowUI, RecycleOption recycle, UICancelOption OnUserCancel, FileSystem.FileOrDirectory FileOrDirectory)
    {
      NativeMethods.ShFileOperationFlags operationFlags = FileSystem.GetOperationFlags(ShowUI);
      if (recycle == RecycleOption.SendToRecycleBin)
        operationFlags |= NativeMethods.ShFileOperationFlags.FOF_ALLOWUNDO;
      FileSystem.ShellFileOperation(NativeMethods.SHFileOperationType.FO_DELETE, operationFlags, FullPath, (string) null, OnUserCancel, FileOrDirectory);
    }

*/
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt, UI = true)]
/*    private static void ShellFileOperation(NativeMethods.SHFileOperationType OperationType, NativeMethods.ShFileOperationFlags OperationFlags, string FullSource, string FullTarget, UICancelOption OnUserCancel, FileSystem.FileOrDirectory FileOrDirectory)
    {
      new UIPermission(UIPermissionWindow.SafeSubWindows).Demand();
      FileIOPermissionAccess access = FileIOPermissionAccess.NoAccess;
      switch (OperationType)
      {
        case NativeMethods.SHFileOperationType.FO_MOVE:
          access = FileIOPermissionAccess.Read | FileIOPermissionAccess.Write;
          break;
        case NativeMethods.SHFileOperationType.FO_COPY:
          access = FileIOPermissionAccess.Read;
          break;
        case NativeMethods.SHFileOperationType.FO_DELETE:
          access = FileIOPermissionAccess.Write;
          break;
      }
      string str = FullSource;
      if ((OperationType == NativeMethods.SHFileOperationType.FO_COPY || OperationType == NativeMethods.SHFileOperationType.FO_MOVE) && str.EndsWith("*", StringComparison.Ordinal))
        str = FileSystem.RemoveEndingSeparator(FullSource.TrimEnd('*'));
      if (FileOrDirectory == FileSystem.FileOrDirectory.Directory)
        FileSystem.DemandDirectoryPermission(str, access);
      else
        new FileIOPermission(access, str).Demand();
      if (OperationType != NativeMethods.SHFileOperationType.FO_DELETE)
      {
        if (FileOrDirectory == FileSystem.FileOrDirectory.Directory)
          FileSystem.DemandDirectoryPermission(FullTarget, FileIOPermissionAccess.Write);
        else
          new FileIOPermission(FileIOPermissionAccess.Write, FullTarget).Demand();
      }
      NativeMethods.SHFILEOPSTRUCT shellOperationInfo = FileSystem.GetShellOperationInfo(OperationType, OperationFlags, FullSource, FullTarget);
      new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
      int errorCode;
      try
      {
        errorCode = NativeMethods.SHFileOperation(ref shellOperationInfo);
        NativeMethods.SHChangeNotify(145439U, 3U, IntPtr.Zero, IntPtr.Zero);
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      if (shellOperationInfo.fAnyOperationsAborted)
      {
        if (OnUserCancel == UICancelOption.ThrowException)
          throw new OperationCanceledException();
      }
      else
      {
        if (errorCode == 0)
          return;
        FileSystem.ThrowWinIOError(errorCode);
      }
    }
*/
    private static NativeMethods.SHFILEOPSTRUCT GetShellOperationInfo(NativeMethods.SHFileOperationType OperationType, NativeMethods.ShFileOperationFlags OperationFlags, string SourcePath, string TargetPath = null)
    {
      return FileSystem.GetShellOperationInfo(OperationType, OperationFlags, new string[1]
      {
        SourcePath
      }, TargetPath);
    }

    private static NativeMethods.SHFILEOPSTRUCT GetShellOperationInfo(NativeMethods.SHFileOperationType OperationType, NativeMethods.ShFileOperationFlags OperationFlags, string[] SourcePaths, string TargetPath = null)
    {
      NativeMethods.SHFILEOPSTRUCT shfileopstruct=new NativeMethods.SHFILEOPSTRUCT();
      shfileopstruct.hwnd = IntPtr.Zero;
      shfileopstruct.wFunc = (uint) OperationType;
      shfileopstruct.fFlags = (ushort) OperationFlags;
      shfileopstruct.pFrom = FileSystem.GetShellPath(SourcePaths);
      shfileopstruct.pTo = TargetPath != null ? FileSystem.GetShellPath(TargetPath) : (string) null;
      shfileopstruct.hNameMappings = IntPtr.Zero;
      try
      {
        shfileopstruct.hwnd = Process.GetCurrentProcess().MainWindowHandle;
      }
      catch (Exception ex)
      {
        if (ex is SecurityException || ex is InvalidOperationException || ex is NotSupportedException)
          shfileopstruct.hwnd = IntPtr.Zero;
        else
          throw;
      }
      shfileopstruct.lpszProgressTitle = string.Empty;
      return shfileopstruct;
    }

    private static NativeMethods.ShFileOperationFlags GetOperationFlags(FileSystem.UIOptionInternal ShowUI)
    {
      NativeMethods.ShFileOperationFlags fileOperationFlags = NativeMethods.ShFileOperationFlags.FOF_NOCONFIRMMKDIR | NativeMethods.ShFileOperationFlags.FOF_NO_CONNECTED_ELEMENTS;
      if (ShowUI == FileSystem.UIOptionInternal.OnlyErrorDialogs)
        fileOperationFlags |= NativeMethods.ShFileOperationFlags.FOF_SILENT | NativeMethods.ShFileOperationFlags.FOF_NOCONFIRMATION;
      return fileOperationFlags;
    }

    private static string GetShellPath(string FullPath)
    {
      return FileSystem.GetShellPath(new string[1]
      {
        FullPath
      });
    }

    private static string GetShellPath(string[] FullPaths)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string[] strArray = FullPaths;
      int index = 0;
      while (index < strArray.Length)
      {
        string str = strArray[index];
        stringBuilder.Append(str + "\0");
        checked { ++index; }
      }
      return stringBuilder.ToString();
    }

    private static void ThrowIfDevicePath(string path)
    {
      if (path.StartsWith("\\\\.\\", StringComparison.Ordinal))
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (path), "IO_DevicePath");
    }

    private static void ThrowWinIOError(int errorCode)
    {
      switch (errorCode)
      {
        case 2:
          throw new FileNotFoundException();
        case 3:
          throw new DirectoryNotFoundException();
        case 5:
          throw new UnauthorizedAccessException();
        case 15:
          throw new DriveNotFoundException();
        case 206:
          throw new PathTooLongException();
        case 995:
        case 1223:
          throw new OperationCanceledException();
        default:
          throw new IOException(new Win32Exception(errorCode).Message, Marshal.GetHRForLastWin32Error());
      }
    }

    private static FileSystem.UIOptionInternal ToUIOptionInternal(UIOption showUI)
    {
      switch (showUI)
      {
        case UIOption.OnlyErrorDialogs:
          return FileSystem.UIOptionInternal.OnlyErrorDialogs;
        case UIOption.AllDialogs:
          return FileSystem.UIOptionInternal.AllDialogs;
        default:
          throw new InvalidEnumArgumentException(nameof (showUI), (int) showUI, typeof (UIOption));
      }
    }

    private static void VerifyDeleteDirectoryOption(string argName, DeleteDirectoryOption argValue)
    {
      if (argValue != DeleteDirectoryOption.DeleteAllContents && argValue != DeleteDirectoryOption.ThrowIfDirectoryNonEmpty)
        throw new InvalidEnumArgumentException(argName, (int) argValue, typeof (DeleteDirectoryOption));
    }

    private static void VerifyRecycleOption(string argName, RecycleOption argValue)
    {
      if (argValue != RecycleOption.DeletePermanently && argValue != RecycleOption.SendToRecycleBin)
        throw new InvalidEnumArgumentException(argName, (int) argValue, typeof (RecycleOption));
    }

    private static void VerifySearchOption(string argName, SearchOption argValue)
    {
      if (argValue != SearchOption.SearchAllSubDirectories && argValue != SearchOption.SearchTopLevelOnly)
        throw new InvalidEnumArgumentException(argName, (int) argValue, typeof (SearchOption));
    }

    private static void VerifyUICancelOption(string argName, UICancelOption argValue)
    {
      if (argValue != UICancelOption.DoNothing && argValue != UICancelOption.ThrowException)
        throw new InvalidEnumArgumentException(argName, (int) argValue, typeof (UICancelOption));
    }

    private enum CopyOrMove
    {
      Copy,
      Move,
    }

    private enum FileOrDirectory
    {
      File,
      Directory,
    }

    private enum UIOptionInternal
    {
      OnlyErrorDialogs = 2,
      AllDialogs = 3,
      NoUI = 4,
    }

    private class DirectoryNode
    {
      private string m_Path;
      private string m_TargetPath;
      private Collection<FileSystem.DirectoryNode> m_SubDirs;

      internal DirectoryNode(string DirectoryPath, string TargetDirectoryPath)
      {
        this.m_Path = DirectoryPath;
        this.m_TargetPath = TargetDirectoryPath;
        this.m_SubDirs = new Collection<FileSystem.DirectoryNode>();
        string[] directories = Directory.GetDirectories(this.m_Path);
        int index = 0;
        while (index < directories.Length)
        {
          string str = directories[index];
          string TargetDirectoryPath1 = System.IO.Path.Combine(this.m_TargetPath, System.IO.Path.GetFileName(str));
          this.m_SubDirs.Add(new FileSystem.DirectoryNode(str, TargetDirectoryPath1));
          checked { ++index; }
        }
      }

      internal string Path
      {
        get
        {
          return this.m_Path;
        }
      }

      internal string TargetPath
      {
        get
        {
          return this.m_TargetPath;
        }
      }

      internal Collection<FileSystem.DirectoryNode> SubDirs
      {
        get
        {
          return this.m_SubDirs;
        }
      }
    }

    private class TextSearchHelper
    {
      private string m_SearchText;
      private bool m_IgnoreCase;
      private Decoder m_Decoder;
      private char[] m_PreviousCharBuffer;
      private bool m_CheckPreamble;
      private byte[] m_Preamble;

      internal TextSearchHelper(Encoding Encoding, string Text, bool IgnoreCase)
      {
        this.m_PreviousCharBuffer = new char[0];
        this.m_CheckPreamble = true;
        this.m_Decoder = Encoding.GetDecoder();
        this.m_Preamble = Encoding.GetPreamble();
        this.m_IgnoreCase = IgnoreCase;
        if (this.m_IgnoreCase)
          this.m_SearchText = Text.ToUpper(CultureInfo.CurrentCulture);
        else
          this.m_SearchText = Text;
      }

      internal bool IsTextFound(byte[] ByteBuffer, int Count)
      {
        int num = 0;
        if (this.m_CheckPreamble)
        {
          if (FileSystem.TextSearchHelper.BytesMatch(ByteBuffer, this.m_Preamble))
          {
            num = this.m_Preamble.Length;
            checked { Count -= this.m_Preamble.Length; }
          }
          this.m_CheckPreamble = false;
          if (Count <= 0)
            return false;
        }
        char[] chars = new char[checked (this.m_PreviousCharBuffer.Length + this.m_Decoder.GetCharCount(ByteBuffer, num, Count) - 1 + 1)];
        Array.Copy((Array) this.m_PreviousCharBuffer, 0, (Array) chars, 0, this.m_PreviousCharBuffer.Length);
        this.m_Decoder.GetChars(ByteBuffer, num, Count, chars, this.m_PreviousCharBuffer.Length);
        if (chars.Length > this.m_SearchText.Length)
        {
          if (this.m_PreviousCharBuffer.Length != this.m_SearchText.Length)
            this.m_PreviousCharBuffer = new char[checked (this.m_SearchText.Length - 1 + 1)];
          Array.Copy((Array) chars, checked (chars.Length - this.m_SearchText.Length), (Array) this.m_PreviousCharBuffer, 0, this.m_SearchText.Length);
        }
        else
          this.m_PreviousCharBuffer = chars;
        if (this.m_IgnoreCase)
          return new string(chars).ToUpper(CultureInfo.CurrentCulture).Contains(this.m_SearchText);
        return new string(chars).Contains(this.m_SearchText);
      }

      private TextSearchHelper()
      {
        this.m_PreviousCharBuffer = new char[0];
        this.m_CheckPreamble = true;
      }

      private static bool BytesMatch(byte[] BigBuffer, byte[] SmallBuffer)
      {
        if (BigBuffer.Length < SmallBuffer.Length | SmallBuffer.Length == 0)
          return false;
        int num1 = 0;
        int num2 = checked (SmallBuffer.Length - 1);
        int index = num1;
        while (index <= num2)
        {
          if ((int) BigBuffer[index] != (int) SmallBuffer[index])
            return false;
          checked { ++index; }
        }
        return true;
      }
    }
  }
}
