﻿// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ExceptionUtils
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>Provides exception handling support for the Visual Basic runtime. This class is not intended to be used from your code.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class ExceptionUtils
  {
    internal const int E_NOTIMPL = -2147467263;
    internal const int E_NOINTERFACE = -2147467262;
    internal const int E_POINTER = -2147467261;
    internal const int E_ABORT = -2147467260;
    internal const int DISP_E_UNKNOWNINTERFACE = -2147352575;
    internal const int DISP_E_MEMBERNOTFOUND = -2147352573;
    internal const int DISP_E_PARAMNOTFOUND = -2147352572;
    internal const int DISP_E_TYPEMISMATCH = -2147352571;
    internal const int DISP_E_UNKNOWNNAME = -2147352570;
    internal const int DISP_E_NONAMEDARGS = -2147352569;
    internal const int DISP_E_BADVARTYPE = -2147352568;
    internal const int DISP_E_OVERFLOW = -2147352566;
    internal const int DISP_E_BADINDEX = -2147352565;
    internal const int DISP_E_UNKNOWNLCID = -2147352564;
    internal const int DISP_E_ARRAYISLOCKED = -2147352563;
    internal const int DISP_E_BADPARAMCOUNT = -2147352562;
    internal const int DISP_E_PARAMNOTOPTIONAL = -2147352561;
    internal const int DISP_E_NOTACOLLECTION = -2147352559;
    internal const int DISP_E_DIVBYZERO = -2147352558;
    internal const int TYPE_E_BUFFERTOOSMALL = -2147319786;
    internal const int TYPE_E_INVDATAREAD = -2147319784;
    internal const int TYPE_E_UNSUPFORMAT = -2147319783;
    internal const int TYPE_E_REGISTRYACCESS = -2147319780;
    internal const int TYPE_E_LIBNOTREGISTERED = -2147319779;
    internal const int TYPE_E_UNDEFINEDTYPE = -2147319769;
    internal const int TYPE_E_QUALIFIEDNAMEDISALLOWED = -2147319768;
    internal const int TYPE_E_INVALIDSTATE = -2147319767;
    internal const int TYPE_E_WRONGTYPEKIND = -2147319766;
    internal const int TYPE_E_ELEMENTNOTFOUND = -2147319765;
    internal const int TYPE_E_AMBIGUOUSNAME = -2147319764;
    internal const int TYPE_E_NAMECONFLICT = -2147319763;
    internal const int TYPE_E_UNKNOWNLCID = -2147319762;
    internal const int TYPE_E_DLLFUNCTIONNOTFOUND = -2147319761;
    internal const int TYPE_E_BADMODULEKIND = -2147317571;
    internal const int TYPE_E_SIZETOOBIG = -2147317563;
    internal const int TYPE_E_TYPEMISMATCH = -2147316576;
    internal const int TYPE_E_OUTOFBOUNDS = -2147316575;
    internal const int TYPE_E_IOERROR = -2147316574;
    internal const int TYPE_E_CANTCREATETMPFILE = -2147316573;
    internal const int TYPE_E_CANTLOADLIBRARY = -2147312566;
    internal const int TYPE_E_INCONSISTENTPROPFUNCS = -2147312509;
    internal const int TYPE_E_CIRCULARTYPE = -2147312508;
    internal const int STG_E_INVALIDFUNCTION = -2147287039;
    internal const int STG_E_FILENOTFOUND = -2147287038;
    internal const int STG_E_PATHNOTFOUND = -2147287037;
    internal const int STG_E_TOOMANYOPENFILES = -2147287036;
    internal const int STG_E_ACCESSDENIED = -2147287035;
    internal const int STG_E_INVALIDHANDLE = -2147287034;
    internal const int STG_E_INSUFFICIENTMEMORY = -2147287032;
    internal const int STG_E_NOMOREFILES = -2147287022;
    internal const int STG_E_DISKISWRITEPROTECTED = -2147287021;
    internal const int STG_E_SEEKERROR = -2147287015;
    internal const int STG_E_WRITEFAULT = -2147287011;
    internal const int STG_E_READFAULT = -2147287010;
    internal const int STG_E_SHAREVIOLATION = -2147287008;
    internal const int STG_E_LOCKVIOLATION = -2147287007;
    internal const int STG_E_FILEALREADYEXISTS = -2147286960;
    internal const int STG_E_MEDIUMFULL = -2147286928;
    internal const int STG_E_INVALIDHEADER = -2147286789;
    internal const int STG_E_INVALIDNAME = -2147286788;
    internal const int STG_E_UNKNOWN = -2147286787;
    internal const int STG_E_UNIMPLEMENTEDFUNCTION = -2147286786;
    internal const int STG_E_INUSE = -2147286784;
    internal const int STG_E_NOTCURRENT = -2147286783;
    internal const int STG_E_REVERTED = -2147286782;
    internal const int STG_E_CANTSAVE = -2147286781;
    internal const int STG_E_OLDFORMAT = -2147286780;
    internal const int STG_E_OLDDLL = -2147286779;
    internal const int STG_E_SHAREREQUIRED = -2147286778;
    internal const int STG_E_NOTFILEBASEDSTORAGE = -2147286777;
    internal const int STG_E_EXTANTMARSHALLINGS = -2147286776;
    internal const int CLASS_E_NOTLICENSED = -2147221230;
    internal const int REGDB_E_CLASSNOTREG = -2147221164;
    internal const int MK_E_UNAVAILABLE = -2147221021;
    internal const int MK_E_INVALIDEXTENSION = -2147221018;
    internal const int MK_E_CANTOPENFILE = -2147221014;
    internal const int CO_E_CLASSSTRING = -2147221005;
    internal const int CO_E_APPNOTFOUND = -2147221003;
    internal const int CO_E_APPDIDNTREG = -2147220994;
    internal const int E_ACCESSDENIED = -2147024891;
    internal const int E_OUTOFMEMORY = -2147024882;
    internal const int E_INVALIDARG = -2147024809;
    internal const int CO_E_SERVER_EXEC_FAILURE = -2146959355;

    private ExceptionUtils()
    {
    }

    internal static Exception MakeException1(int hr, string Parm1)
    {
      string sMsg = hr <= 0 || hr > (int) ushort.MaxValue ? "" : Utils.GetResourceString((vbErrors) hr);
      int length = sMsg.IndexOf("%1", StringComparison.OrdinalIgnoreCase);
      if (length >= 0)
        sMsg = sMsg.Substring(0, length) + Parm1 + sMsg.Substring(checked (length + 2));
      return ExceptionUtils.VbMakeExceptionEx(hr, sMsg);
    }

    internal static Exception VbMakeException(int hr)
    {
      string sMsg = hr <= 0 || hr > (int) ushort.MaxValue ? "" : Utils.GetResourceString((vbErrors) hr);
      return ExceptionUtils.VbMakeExceptionEx(hr, sMsg);
    }

    internal static Exception VbMakeException(Exception ex, int hr)
    {
      Information.Err().SetUnmappedError(hr);
      return ex;
    }

    internal static Exception VbMakeExceptionEx(int Number, string sMsg)
    {
      bool VBDefinedError=false;
      Exception exception = ExceptionUtils.BuildException(Number, sMsg, ref VBDefinedError);
      if (VBDefinedError)
        Information.Err().SetUnmappedError(Number);
      return exception;
    }

    internal static Exception BuildException(int Number, string Description, ref bool VBDefinedError)
    {
      VBDefinedError = true;
      switch (Number)
      {
        case -2147467261:
          return (Exception) new AccessViolationException();
        case 0:
          return (Exception) null;
        case 3:
        case 20:
        case 94:
        case 100:
          return (Exception) new InvalidOperationException(Description);
        case 5:
        case 446:
        case 448:
        case 449:
          return (Exception) new ArgumentException(Description);
        case 6:
          return (Exception) new OverflowException(Description);
        case 7:
        case 14:
          return (Exception) new OutOfMemoryException(Description);
        case 9:
          return (Exception) new IndexOutOfRangeException(Description);
        case 11:
          return (Exception) new DivideByZeroException(Description);
        case 13:
          return (Exception) new InvalidCastException(Description);
        case 28:
          return (Exception) new StackOverflowException(Description);
        case 48:
          return (Exception) new TypeLoadException(Description);
        case 52:
        case 54:
        case 55:
        case 57:
        case 58:
        case 59:
        case 61:
        case 63:
        case 67:
        case 68:
        case 70:
        case 71:
        case 74:
        case 75:
          return (Exception) new IOException(Description);
        case 53:
          return (Exception) new FileNotFoundException(Description);
        case 62:
          return (Exception) new EndOfStreamException(Description);
        case 76:
        case 432:
          return (Exception) new FileNotFoundException(Description);
        case 91:
          return (Exception) new NullReferenceException(Description);
        case 422:
          return (Exception) new MissingFieldException(Description);
        case 429:
        case 462:
          return new Exception(Description);
        case 438:
          return (Exception) new MissingMemberException(Description);
        default:
          VBDefinedError = false;
          return new Exception(Description);
      }
    }

    internal static ArgumentException GetArgumentExceptionWithArgName(string ArgumentName, string ResourceID, params string[] PlaceHolders)
    {
      return new ArgumentException(Utils.GetResourceString(ResourceID, PlaceHolders), ArgumentName);
    }

    internal static ArgumentNullException GetArgumentNullException(string ArgumentName)
    {
      return new ArgumentNullException(ArgumentName, Utils.GetResourceString("General_ArgumentNullException"));
    }

    internal static ArgumentNullException GetArgumentNullException(string ArgumentName, string ResourceID, params string[] PlaceHolders)
    {
      return new ArgumentNullException(ArgumentName, Utils.GetResourceString(ResourceID, PlaceHolders));
    }

    internal static DirectoryNotFoundException GetDirectoryNotFoundException(string ResourceID, params string[] PlaceHolders)
    {
      return new DirectoryNotFoundException(Utils.GetResourceString(ResourceID, PlaceHolders));
    }

    internal static FileNotFoundException GetFileNotFoundException(string FileName, string ResourceID, params string[] PlaceHolders)
    {
      return new FileNotFoundException(Utils.GetResourceString(ResourceID, PlaceHolders), FileName);
    }

    internal static InvalidOperationException GetInvalidOperationException(string ResourceID, params string[] PlaceHolders)
    {
      return new InvalidOperationException(Utils.GetResourceString(ResourceID, PlaceHolders));
    }

    internal static IOException GetIOException(string ResourceID, params string[] PlaceHolders)
    {
      return new IOException(Utils.GetResourceString(ResourceID, PlaceHolders));
    }

    internal static Win32Exception GetWin32Exception(string ResourceID, params string[] PlaceHolders)
    {
      return new Win32Exception(Marshal.GetLastWin32Error(), Utils.GetResourceString(ResourceID, PlaceHolders));
    }
  }
}
