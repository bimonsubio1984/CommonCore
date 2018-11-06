// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.NativeTypes
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal sealed class NativeTypes
  {
    internal static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);
    internal const int GW_HWNDFIRST = 0;
    internal const int GW_HWNDLAST = 1;
    internal const int GW_HWNDNEXT = 2;
    internal const int GW_HWNDPREV = 3;
    internal const int GW_OWNER = 4;
    internal const int GW_CHILD = 5;
    internal const int GW_MAX = 5;
    internal const int STARTF_USESHOWWINDOW = 1;
    internal const int NORMAL_PRIORITY_CLASS = 32;
    internal const int LCMAP_TRADITIONAL_CHINESE = 67108864;
    internal const int LCMAP_SIMPLIFIED_CHINESE = 33554432;
    internal const int LCMAP_UPPERCASE = 512;
    internal const int LCMAP_LOWERCASE = 256;
    internal const int LCMAP_FULLWIDTH = 8388608;
    internal const int LCMAP_HALFWIDTH = 4194304;
    internal const int LCMAP_KATAKANA = 2097152;
    internal const int LCMAP_HIRAGANA = 1048576;
    internal const int ERROR_FILE_NOT_FOUND = 2;
    internal const int ERROR_PATH_NOT_FOUND = 3;
    internal const int ERROR_ACCESS_DENIED = 5;
    internal const int ERROR_ALREADY_EXISTS = 183;
    internal const int ERROR_FILENAME_EXCED_RANGE = 206;
    internal const int ERROR_INVALID_DRIVE = 15;
    internal const int ERROR_INVALID_PARAMETER = 87;
    internal const int ERROR_SHARING_VIOLATION = 32;
    internal const int ERROR_FILE_EXISTS = 80;
    internal const int ERROR_OPERATION_ABORTED = 995;
    internal const int ERROR_CANCELLED = 1223;

    private NativeTypes()
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class SECURITY_ATTRIBUTES : IDisposable
    {
      public int nLength;
      public IntPtr lpSecurityDescriptor;
      public bool bInheritHandle;

      public SECURITY_ATTRIBUTES()
      {
        this.nLength = Marshal.SizeOf(typeof (NativeTypes.SECURITY_ATTRIBUTES));
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      public void Dispose()
      {
        if (this.lpSecurityDescriptor != IntPtr.Zero)
        {
          UnsafeNativeMethods.LocalFree(this.lpSecurityDescriptor);
          this.lpSecurityDescriptor = IntPtr.Zero;
        }
        GC.SuppressFinalize((object) this);
      }

      ~SECURITY_ATTRIBUTES()
      {
        this.Dispose();
        // ISSUE: explicit finalizer call
        //base.Finalize();
      }
    }

        [SuppressUnmanagedCodeSecurity]
        [StructLayout(LayoutKind.Sequential)]
        internal sealed class PROCESS_INFORMATION : IDisposable
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;

            public PROCESS_INFORMATION()
            {
                this.hProcess = IntPtr.Zero;
                this.hThread = IntPtr.Zero;
            }

            ~PROCESS_INFORMATION()
            {
                ((IDisposable)this).Dispose();
            }


      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      void IDisposable.Dispose()
      {
        if (this.hProcess != IntPtr.Zero && this.hProcess != NativeTypes.INVALID_HANDLE)
        {
          NativeMethods.CloseHandle(this.hProcess);
          this.hProcess = NativeTypes.INVALID_HANDLE;
        }
        if (!(this.hThread != IntPtr.Zero) && this.hThread != NativeTypes.INVALID_HANDLE)
        {
          NativeMethods.CloseHandle(this.hThread);
          this.hThread = NativeTypes.INVALID_HANDLE;
        }
        GC.SuppressFinalize((object) this);
      }
    }

    [SuppressUnmanagedCodeSecurity]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal sealed class STARTUPINFO : IDisposable
    {
      public int cb;
      public IntPtr lpReserved;
      public IntPtr lpDesktop;
      public IntPtr lpTitle;
      public int dwX;
      public int dwY;
      public int dwXSize;
      public int dwYSize;
      public int dwXCountChars;
      public int dwYCountChars;
      public int dwFillAttribute;
      public int dwFlags;
      public short wShowWindow;
      public short cbReserved2;
      public IntPtr lpReserved2;
      public IntPtr hStdInput;
      public IntPtr hStdOutput;
      public IntPtr hStdError;

      public STARTUPINFO()
      {
        this.lpReserved = IntPtr.Zero;
        this.lpDesktop = IntPtr.Zero;
        this.lpTitle = IntPtr.Zero;
        this.lpReserved2 = IntPtr.Zero;
        this.hStdInput = IntPtr.Zero;
        this.hStdOutput = IntPtr.Zero;
        this.hStdError = IntPtr.Zero;
      }

      ~STARTUPINFO()
      {
                ((IDisposable)this).Dispose();
      }

      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      void IDisposable.Dispose()
      {
        if ((this.dwFlags & 256) != 0)
        {
          if (this.hStdInput != IntPtr.Zero && this.hStdInput != NativeTypes.INVALID_HANDLE)
          {
            NativeMethods.CloseHandle(this.hStdInput);
            this.hStdInput = NativeTypes.INVALID_HANDLE;
          }
          if (this.hStdOutput != IntPtr.Zero && this.hStdOutput != NativeTypes.INVALID_HANDLE)
          {
            NativeMethods.CloseHandle(this.hStdOutput);
            this.hStdOutput = NativeTypes.INVALID_HANDLE;
          }
          if (this.hStdError != IntPtr.Zero && this.hStdError != NativeTypes.INVALID_HANDLE)
          {
            NativeMethods.CloseHandle(this.hStdError);
            this.hStdError = NativeTypes.INVALID_HANDLE;
          }
        }
        GC.SuppressFinalize((object) this);
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class SystemTime
    {
      public short wYear;
      public short wMonth;
      public short wDayOfWeek;
      public short wDay;
      public short wHour;
      public short wMinute;
      public short wSecond;
      public short wMilliseconds;
    }

    [Flags]
    internal enum MoveFileExFlags
    {
      MOVEFILE_REPLACE_EXISTING = 1,
      MOVEFILE_COPY_ALLOWED = 2,
      MOVEFILE_DELAY_UNTIL_REBOOT = 4,
      MOVEFILE_WRITE_THROUGH = 8,
    }
  }
}
