// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyServices.FileSystemProxy
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using Ported.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace Ported.VisualBasic.MyServices
{
  /// <summary>Provides properties and methods for working with drives, files, and directories.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class FileSystemProxy
  {
    private SpecialDirectoriesProxy m_SpecialDirectoriesProxy;

    /// <summary>Returns a read-only collection of all available drive names.</summary>
    /// <returns>A read-only collection of all available drives as <see cref="T:System.IO.DriveInfo" /> objects.</returns>
    public ReadOnlyCollection<DriveInfo> Drives
    {
      get
      {
        return Ported.VisualBasic.FileIO.FileSystem.Drives;
      }
    }

    /// <summary>Gets an object that provides properties for accessing commonly referenced directories.</summary>
    /// <returns>This property returns the <see cref="T:Ported.VisualBasic.FileIO.SpecialDirectories" /> object for the computer.</returns>
    public SpecialDirectoriesProxy SpecialDirectories
    {
      get
      {
        if (this.m_SpecialDirectoriesProxy == null)
          this.m_SpecialDirectoriesProxy = new SpecialDirectoriesProxy();
        return this.m_SpecialDirectoriesProxy;
      }
    }

    /// <summary>Gets or sets the current directory.</summary>
    /// <returns>The current directory for file I/O operations.</returns>
    /// <exception cref="T:System.IO.DirectoryNotFoundException">The path is not valid.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user lacks necessary permissions.</exception>
    public string CurrentDirectory
    {
      get
      {
        return Ported.VisualBasic.FileIO.FileSystem.CurrentDirectory;
      }
      set
      {
        Ported.VisualBasic.FileIO.FileSystem.CurrentDirectory = value;
      }
    }

    /// <summary>Returns <see langword="True" /> if the specified directory exists.</summary>
    /// <param name="directory">Path of the directory. </param>
    /// <returns>
    /// <see langword="True" /> if the directory exists; otherwise <see langword="False" />.</returns>
    public bool DirectoryExists(string directory)
    {
      return Ported.VisualBasic.FileIO.FileSystem.DirectoryExists(directory);
    }

    /// <summary>Returns <see langword="True" /> if the specified file exists.</summary>
    /// <param name="file">Name and path of the file. </param>
    /// <returns>Returns <see langword="True" /> if the file exists; otherwise this method returns <see langword="False" />.</returns>
    /// <exception cref="T:System.ArgumentException">The name of the file ends with a backslash (\).</exception>
    public bool FileExists(string file)
    {
      return Ported.VisualBasic.FileIO.FileSystem.FileExists(file);
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
    public void CreateDirectory(string directory)
    {
      Ported.VisualBasic.FileIO.FileSystem.CreateDirectory(directory);
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
    public DirectoryInfo GetDirectoryInfo(string directory)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetDirectoryInfo(directory);
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
    public FileInfo GetFileInfo(string file)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetFileInfo(file);
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
    public DriveInfo GetDriveInfo(string drive)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetDriveInfo(drive);
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
    public ReadOnlyCollection<string> GetFiles(string directory)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetFiles(directory);
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
    public ReadOnlyCollection<string> GetFiles(string directory, Ported.VisualBasic.FileIO.SearchOption searchType, params string[] wildcards)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetFiles(directory, searchType, wildcards);
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
    public ReadOnlyCollection<string> GetDirectories(string directory)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetDirectories(directory);
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
    public ReadOnlyCollection<string> GetDirectories(string directory, Ported.VisualBasic.FileIO.SearchOption searchType, params string[] wildcards)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetDirectories(directory, searchType, wildcards);
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
    public ReadOnlyCollection<string> FindInFiles(string directory, string containsText, bool ignoreCase, Ported.VisualBasic.FileIO.SearchOption searchType)
    {
      return Ported.VisualBasic.FileIO.FileSystem.FindInFiles(directory, containsText, ignoreCase, searchType);
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
    public ReadOnlyCollection<string> FindInFiles(string directory, string containsText, bool ignoreCase, Ported.VisualBasic.FileIO.SearchOption searchType, params string[] fileWildcards)
    {
      return Ported.VisualBasic.FileIO.FileSystem.FindInFiles(directory, containsText, ignoreCase, searchType, fileWildcards);
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
    public string GetParentPath(string path)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetParentPath(path);
    }

    /// <summary>Combines two paths and returns a properly formatted path.</summary>
    /// <param name="baseDirectory">
    /// <see langword="String" />. First path to be combined. </param>
    /// <param name="relativePath">
    /// <see langword="String" />. Second path to be combined. </param>
    /// <returns>The combination of the specified paths.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="baseDirectory" /> or <paramref name="relativePath" /> are malformed paths.</exception>
    public string CombinePath(string baseDirectory, string relativePath)
    {
      return Ported.VisualBasic.FileIO.FileSystem.CombinePath(baseDirectory, relativePath);
    }

    /// <summary>Parses the file name out of the path provided.</summary>
    /// <param name="path">Required. Path to be parsed. <see langword="String" />.</param>
    /// <returns>The file name from the specified path.</returns>
    public string GetName(string path)
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetName(path);
    }

    /// <summary>Creates a uniquely named zero-byte temporary file on disk and returns the full path of that file.</summary>
    /// <returns>
    /// <see langword="String" /> containing the full path of the temporary file.</returns>
    public string GetTempFileName()
    {
      return Ported.VisualBasic.FileIO.FileSystem.GetTempFileName();
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
    public string ReadAllText(string file)
    {
      return Ported.VisualBasic.FileIO.FileSystem.ReadAllText(file);
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
    public string ReadAllText(string file, Encoding encoding)
    {
      return Ported.VisualBasic.FileIO.FileSystem.ReadAllText(file, encoding);
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
    public byte[] ReadAllBytes(string file)
    {
      return Ported.VisualBasic.FileIO.FileSystem.ReadAllBytes(file);
    }

    /// <summary>Writes text to a file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="text">Text to be written to file. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents of the file; <see langword="False" /> to overwrite the contents of the file. Default is <see langword="False" />. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public void WriteAllText(string file, string text, bool append)
    {
      Ported.VisualBasic.FileIO.FileSystem.WriteAllText(file, text, append);
    }

    /// <summary>Writes text to a file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="text">Text to be written to file. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents of the file; <see langword="False" /> to overwrite the contents of the file. Default is <see langword="False" />. </param>
    /// <param name="encoding">What encoding to use when writing to file. Default is UTF-8.</param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\); it ends with a trailing slash.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">The file is in use by another process, or an I/O error occurs.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.OutOfMemoryException">There is not enough memory to write the string to buffer.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    public void WriteAllText(string file, string text, bool append, Encoding encoding)
    {
      Ported.VisualBasic.FileIO.FileSystem.WriteAllText(file, text, append, encoding);
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
    public void WriteAllBytes(string file, byte[] data, bool append)
    {
      Ported.VisualBasic.FileIO.FileSystem.WriteAllBytes(file, data, append);
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
    public void CopyFile(string sourceFileName, string destinationFileName)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName);
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
    public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName, overwrite);
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
    public void CopyFile(string sourceFileName, string destinationFileName, UIOption showUI)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName, showUI);
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
    public void CopyFile(string sourceFileName, string destinationFileName, UIOption showUI, UICancelOption onUserCancel)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName, showUI, onUserCancel);
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
    public void MoveFile(string sourceFileName, string destinationFileName)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName);
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
    public void MoveFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName, overwrite);
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
    public void MoveFile(string sourceFileName, string destinationFileName, UIOption showUI)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName, showUI);
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
    public void MoveFile(string sourceFileName, string destinationFileName, UIOption showUI, UICancelOption onUserCancel)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveFile(sourceFileName, destinationFileName, showUI, onUserCancel);
    }

    /// <summary>Copies a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory should be copied.</param>
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
    public void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName);
    }

    /// <summary>Copies a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory should be copied.</param>
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
    public void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwrite)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, overwrite);
    }

    /// <summary>Copies a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory should be copied.</param>
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
    public void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, showUI);
    }

    /// <summary>Copies a directory to another directory.</summary>
    /// <param name="sourceDirectoryName">The directory to be copied.</param>
    /// <param name="destinationDirectoryName">The location to which the directory should be copied.</param>
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
    public void CopyDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI, UICancelOption onUserCancel)
    {
      Ported.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName, showUI, onUserCancel);
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
    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName);
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
    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName, bool overwrite)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName, overwrite);
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
    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName, showUI);
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
    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName, UIOption showUI, UICancelOption onUserCancel)
    {
      Ported.VisualBasic.FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName, showUI, onUserCancel);
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
    public void DeleteFile(string file)
    {
      Ported.VisualBasic.FileIO.FileSystem.DeleteFile(file);
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
    public void DeleteFile(string file, UIOption showUI, RecycleOption recycle)
    {
      Ported.VisualBasic.FileIO.FileSystem.DeleteFile(file, showUI, recycle);
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
    public void DeleteFile(string file, UIOption showUI, RecycleOption recycle, UICancelOption onUserCancel)
    {
      Ported.VisualBasic.FileIO.FileSystem.DeleteFile(file, showUI, recycle, onUserCancel);
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
    public void DeleteDirectory(string directory, DeleteDirectoryOption onDirectoryNotEmpty)
    {
      Ported.VisualBasic.FileIO.FileSystem.DeleteDirectory(directory, onDirectoryNotEmpty);
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
    public void DeleteDirectory(string directory, UIOption showUI, RecycleOption recycle)
    {
      Ported.VisualBasic.FileIO.FileSystem.DeleteDirectory(directory, showUI, recycle);
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
    public void DeleteDirectory(string directory, UIOption showUI, RecycleOption recycle, UICancelOption onUserCancel)
    {
      Ported.VisualBasic.FileIO.FileSystem.DeleteDirectory(directory, showUI, recycle, onUserCancel);
    }

    /// <summary>Renames a file.</summary>
    /// <param name="file">File to be renamed. </param>
    /// <param name="newName">New name of file. </param>
    /// <exception cref="T:System.ArgumentException">The path is not valid for one of the following reasons: it is a zero-length string; it contains only white space; it contains invalid characters; or it is a device path (starts with \\.\).</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="newName" /> contains path information or ends with a backslash (\).</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="file" /> is <see langword="Nothing" />.-or-
    /// <paramref name="newName" /> is <see langword="Nothing" /> or an empty string.</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="T:System.IO.IOException">There is an existing file or directory with the name specified in <paramref name="newName" />.</exception>
    /// <exception cref="T:System.IO.PathTooLongException">The path exceeds the system-defined maximum length.</exception>
    /// <exception cref="T:System.NotSupportedException">A file or directory name in the path contains a colon (:) or is in an invalid format.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to view the path.</exception>
    /// <exception cref="T:System.UnauthorizedAccessException">The user does not have required permission.</exception>
    public void RenameFile(string file, string newName)
    {
      Ported.VisualBasic.FileIO.FileSystem.RenameFile(file, newName);
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
    public void RenameDirectory(string directory, string newName)
    {
      Ported.VisualBasic.FileIO.FileSystem.RenameDirectory(directory, newName);
    }

    /// <summary>The <see langword="OpenTextFieldParser" /> method allows you to create a <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object, which provides a way to easily and efficiently parse structured text files, such as logs. The <see langword="TextFieldParser" /> object can be used to read both delimited and fixed-width files.</summary>
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
    public TextFieldParser OpenTextFieldParser(string file)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFieldParser(file);
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
    public TextFieldParser OpenTextFieldParser(string file, params string[] delimiters)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFieldParser(file, delimiters);
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
    public TextFieldParser OpenTextFieldParser(string file, params int[] fieldWidths)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFieldParser(file, fieldWidths);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamReader" /> object to read from a file.</summary>
    /// <param name="file">File to be read. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamReader" /> object to read from the file</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a backslash (\).</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to read from the file.</exception>
    public StreamReader OpenTextFileReader(string file)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFileReader(file);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamReader" /> object to read from a file.</summary>
    /// <param name="file">File to be read. </param>
    /// <param name="encoding">The encoding to use for the file contents. Default is ASCII. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamReader" /> object to read from the file</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a backslash (\).</exception>
    /// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
    /// <exception cref="T:System.Security.SecurityException">The user lacks necessary permissions to read from the file.</exception>
    public StreamReader OpenTextFileReader(string file, Encoding encoding)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFileReader(file, encoding);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamWriter" /> object to write to the specified file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents of the file; <see langword="False" /> to overwrite the contents of the file. Default is <see langword="False" />. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamWriter" /> object to write to the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a trailing slash.</exception>
    public StreamWriter OpenTextFileWriter(string file, bool append)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFileWriter(file, append);
    }

    /// <summary>Opens a <see cref="T:System.IO.StreamWriter" /> to write to the specified file.</summary>
    /// <param name="file">File to be written to. </param>
    /// <param name="append">
    /// <see langword="True" /> to append to the contents in the file; <see langword="False" /> to overwrite the contents of the file. Default is <see langword="False" />. </param>
    /// <param name="encoding">Encoding to be used in writing to the file. Default is ASCII. </param>
    /// <returns>
    /// <see cref="T:System.IO.StreamWriter" /> object to write to the specified file.</returns>
    /// <exception cref="T:System.ArgumentException">The file name ends with a trailing slash.</exception>
    public StreamWriter OpenTextFileWriter(string file, bool append, Encoding encoding)
    {
      return Ported.VisualBasic.FileIO.FileSystem.OpenTextFileWriter(file, append, encoding);
    }

    internal FileSystemProxy()
    {
      this.m_SpecialDirectoriesProxy = (SpecialDirectoriesProxy) null;
    }
  }
}

*/