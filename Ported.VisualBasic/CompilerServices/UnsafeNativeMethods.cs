// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.UnsafeNativeMethods
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Ported.VisualBasic.CompilerServices
{
  [SuppressUnmanagedCodeSecurity]
  [ComVisible(false)]
  internal sealed class UnsafeNativeMethods
  {
    public const int MEMBERID_NIL = 0;
    public const int LCID_US_ENGLISH = 1033;

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
    internal static extern int LCMapStringA(int Locale, int dwMapFlags, [MarshalAs(UnmanagedType.LPArray)] byte[] lpSrcStr, int cchSrc, [MarshalAs(UnmanagedType.LPArray)] byte[] lpDestStr, int cchDest);

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern int LCMapString(int Locale, int dwMapFlags, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpSrcStr, int cchSrc, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDestStr, int cchDest);

    [DllImport("oleaut32", CharSet = CharSet.Unicode)]
    internal static extern int VarParseNumFromStr([MarshalAs(UnmanagedType.LPWStr), In] string str, int lcid, int dwFlags, [MarshalAs(UnmanagedType.LPArray)] byte[] numprsPtr, [MarshalAs(UnmanagedType.LPArray)] byte[] digits);

    [DllImport("oleaut32", CharSet = CharSet.Unicode, PreserveSig = false)]
    internal static extern object VarNumFromParseNum([MarshalAs(UnmanagedType.LPArray)] byte[] numprsPtr, [MarshalAs(UnmanagedType.LPArray)] byte[] DigitArray, int dwVtBits);

    [DllImport("oleaut32", CharSet = CharSet.Unicode, PreserveSig = false)]
    internal static extern void VariantChangeType(out object dest, [In] ref object Src, short wFlags, short vt);

    [DllImport("user32", CharSet = CharSet.Unicode)]
    internal static extern int MessageBeep(int uType);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int SetLocalTime(NativeTypes.SystemTime systime);

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true, BestFitMapping = false)]
    internal static extern int MoveFile([MarshalAs(UnmanagedType.LPTStr), In] string lpExistingFileName, [MarshalAs(UnmanagedType.LPTStr), In] string lpNewFileName);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    internal static extern int GetLogicalDrives();

    [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
    internal static extern IntPtr CreateFileMapping(HandleRef hFile, [MarshalAs(UnmanagedType.LPStruct)] NativeTypes.SECURITY_ATTRIBUTES lpAttributes, int flProtect, int dwMaxSizeHi, int dwMaxSizeLow, string lpName);

    [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

    [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr MapViewOfFile(HandleRef hFileMapping, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap);

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool UnmapViewOfFile(HandleRef pvBaseAddress);

    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    internal static extern short GetKeyState(int KeyCode);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern IntPtr LocalFree(IntPtr LocalHandle);

    [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetDiskFreeSpaceEx(string Directory, ref long UserSpaceFree, ref long TotalUserSpace, ref long TotalFreeSpace);

    private UnsafeNativeMethods()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public enum tagSYSKIND
    {
      SYS_WIN16 = 0,
      SYS_MAC = 2,
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct tagTLIBATTR
    {
      public Guid guid;
      public int lcid;
      public UnsafeNativeMethods.tagSYSKIND syskind;
      [MarshalAs(UnmanagedType.U2)]
      public short wMajorVerNum;
      [MarshalAs(UnmanagedType.U2)]
      public short wMinorVerNum;
      [MarshalAs(UnmanagedType.U2)]
      public short wLibFlags;
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00020403-0000-0000-C000-000000000046")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ComImport]
    public interface ITypeComp
    {
      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void RemoteBind([MarshalAs(UnmanagedType.LPWStr), In] string szName, [MarshalAs(UnmanagedType.U4), In] int lHashVal, [MarshalAs(UnmanagedType.U2), In] short wFlags, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray), Out] System.Runtime.InteropServices.ComTypes.DESCKIND[] pDescKind, [MarshalAs(UnmanagedType.LPArray), Out] System.Runtime.InteropServices.ComTypes.FUNCDESC[] ppFuncDesc, [MarshalAs(UnmanagedType.LPArray), Out] System.Runtime.InteropServices.ComTypes.VARDESC[] ppVarDesc, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeComp[] ppTypeComp, [MarshalAs(UnmanagedType.LPArray), Out] int[] pDummy);

      void RemoteBindType([MarshalAs(UnmanagedType.LPWStr), In] string szName, [MarshalAs(UnmanagedType.U4), In] int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Guid("00020400-0000-0000-C000-000000000046")]
    [ComImport]
    public interface IDispatch
    {
      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetTypeInfoCount();

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetTypeInfo([In] int index, [In] int lcid, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.ITypeInfo pTypeInfo);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetIDsOfNames();

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int Invoke();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00020401-0000-0000-C000-000000000046")]
    [ComImport]
    public interface ITypeInfo
    {
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetTypeAttr(out IntPtr pTypeAttr);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetTypeComp(out UnsafeNativeMethods.ITypeComp pTComp);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetFuncDesc([MarshalAs(UnmanagedType.U4), In] int index, out IntPtr pFuncDesc);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetVarDesc([MarshalAs(UnmanagedType.U4), In] int index, out IntPtr pVarDesc);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetNames([In] int memid, [MarshalAs(UnmanagedType.LPArray), Out] string[] rgBstrNames, [MarshalAs(UnmanagedType.U4), In] int cMaxNames, [MarshalAs(UnmanagedType.U4)] out int cNames);

      [Obsolete("Bad signature, second param type should be Byref. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetRefTypeOfImplType([MarshalAs(UnmanagedType.U4), In] int index, out int pRefType);

      [Obsolete("Bad signature, second param type should be Byref. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetImplTypeFlags([MarshalAs(UnmanagedType.U4), In] int index, [Out] int pImplTypeFlags);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetIDsOfNames([In] IntPtr rgszNames, [MarshalAs(UnmanagedType.U4), In] int cNames, out IntPtr pMemId);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int Invoke();

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetDocumentation([In] int memid, [MarshalAs(UnmanagedType.BStr)] out string pBstrName, [MarshalAs(UnmanagedType.BStr)] out string pBstrDocString, [MarshalAs(UnmanagedType.U4)] out int pdwHelpContext, [MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetDllEntry([In] int memid, [In] System.Runtime.InteropServices.ComTypes.INVOKEKIND invkind, [MarshalAs(UnmanagedType.BStr), Out] string pBstrDllName, [MarshalAs(UnmanagedType.BStr), Out] string pBstrName, [MarshalAs(UnmanagedType.U2), Out] short pwOrdinal);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetRefTypeInfo([In] IntPtr hreftype, out UnsafeNativeMethods.ITypeInfo pTypeInfo);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int AddressOfMember();

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int CreateInstance([In] ref IntPtr pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown), Out] object ppvObj);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetMops([In] int memid, [MarshalAs(UnmanagedType.BStr), Out] string pBstrMops);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      int GetContainingTypeLib([MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeLib[] ppTLib, [MarshalAs(UnmanagedType.LPArray), Out] int[] pIndex);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      void ReleaseTypeAttr(IntPtr typeAttr);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      void ReleaseFuncDesc(IntPtr funcDesc);

      [MethodImpl(MethodImplOptions.PreserveSig)]
      void ReleaseVarDesc(IntPtr varDesc);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Guid("B196B283-BAB4-101A-B69C-00AA00341D07")]
    [ComImport]
    public interface IProvideClassInfo
    {
      [return: MarshalAs(UnmanagedType.Interface)]
      UnsafeNativeMethods.ITypeInfo GetClassInfo();
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Guid("00020402-0000-0000-C000-000000000046")]
    [ComImport]
    public interface ITypeLib
    {
      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void RemoteGetTypeInfoCount([MarshalAs(UnmanagedType.LPArray), Out] int[] pcTInfo);

      void GetTypeInfo([MarshalAs(UnmanagedType.U4), In] int index, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo);

      void GetTypeInfoType([MarshalAs(UnmanagedType.U4), In] int index, [MarshalAs(UnmanagedType.LPArray), Out] System.Runtime.InteropServices.ComTypes.TYPEKIND[] pTKind);

      void GetTypeInfoOfGuid([In] ref Guid guid, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void RemoteGetLibAttr([MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.tagTLIBATTR[] ppTLibAttr, [MarshalAs(UnmanagedType.LPArray), Out] int[] pDummy);

      void GetTypeComp([MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeComp[] ppTComp);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void RemoteGetDocumentation(int index, [MarshalAs(UnmanagedType.U4), In] int refPtrFlags, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrName, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrDocString, [MarshalAs(UnmanagedType.LPArray), Out] int[] pdwHelpContext, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrHelpFile);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void RemoteIsName([MarshalAs(UnmanagedType.LPWStr), In] string szNameBuf, [MarshalAs(UnmanagedType.U4), In] int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] IntPtr[] pfName, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrLibName);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void RemoteFindName([MarshalAs(UnmanagedType.LPWStr), In] string szNameBuf, [MarshalAs(UnmanagedType.U4), In] int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray), Out] int[] rgMemId, [MarshalAs(UnmanagedType.LPArray), In, Out] short[] pcFound, [MarshalAs(UnmanagedType.LPArray), Out] string[] pBstrLibName);

      [Obsolete("Bad signature. Fix and verify signature before use.", true)]
      void LocalReleaseTLibAttr();
    }
  }
}
