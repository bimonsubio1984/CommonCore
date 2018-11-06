// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.SafeNativeMethods
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  [ComVisible(false)]
  [SuppressUnmanagedCodeSecurity]
  internal sealed class SafeNativeMethods
  {
    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool IsWindowEnabled(IntPtr hwnd);

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool IsWindowVisible(IntPtr hwnd);

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpdwProcessId);

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern void GetLocalTime(NativeTypes.SystemTime systime);

    private SafeNativeMethods()
    {
    }
  }
}
