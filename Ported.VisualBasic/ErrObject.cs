// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ErrObject
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="ErrObject" /> module contains properties and procedures used to identify and handle run-time errors using the <see langword="Err" /> object. </summary>
  public sealed class ErrObject
  {
    private Exception m_curException;
    private int m_curErl;
    private int m_curNumber;
    private string m_curSource;
    private string m_curDescription;
    private string m_curHelpFile;
    private int m_curHelpContext;
    private bool m_NumberIsSet;
    private bool m_ClearOnCapture;
    private bool m_SourceIsSet;
    private bool m_DescriptionIsSet;
    private bool m_HelpFileIsSet;
    private bool m_HelpContextIsSet;

    internal ErrObject()
    {
      this.Clear();
    }

    /// <summary>Returns an integer indicating the line number of the last executed statement. Read-only.</summary>
    /// <returns>Returns an integer indicating the line number of the last executed statement. Read-only.</returns>
    public int Erl
    {
      get
      {
        return this.m_curErl;
      }
    }

    /// <summary>Returns or sets a numeric value specifying an error. Read/write.</summary>
    /// <returns>Returns or sets a numeric value specifying an error. Read/write.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is greater than 65535.</exception>
    public int Number
    {
      get
      {
        if (this.m_NumberIsSet)
          return this.m_curNumber;
        if (this.m_curException == null)
          return 0;
        this.Number = this.MapExceptionToNumber(this.m_curException);
        return this.m_curNumber;
      }
      set
      {
        this.m_curNumber = this.MapErrorNumber(value);
        this.m_NumberIsSet = true;
      }
    }

    /// <summary>Returns or sets a <see langword="String" /> expression specifying the name of the object or application that originally generated the error. Read/write.</summary>
    /// <returns>Returns or sets a <see langword="String" /> expression specifying the name of the object or application that originally generated the error. Read/write.</returns>
    public string Source
    {
      get
      {
        if (this.m_SourceIsSet)
          return this.m_curSource;
        if (this.m_curException == null)
          return "";
        this.Source = this.m_curException.Source;
        return this.m_curSource;
      }
      set
      {
        this.m_curSource = value;
        this.m_SourceIsSet = true;
      }
    }

    private string FilterDefaultMessage(string Msg)
    {
      if (this.m_curException == null)
        return Msg;
      int number = this.Number;
      if (Msg == null || Msg.Length == 0)
        Msg = Utils.GetResourceString("ID" + Conversions.ToString(number));
      else if (string.CompareOrdinal("Exception from HRESULT: 0x", 0, Msg, 0, Math.Min(Msg.Length, 26)) == 0)
      {
        string resourceString = Utils.GetResourceString("ID" + Conversions.ToString(this.m_curNumber), false);
        if (resourceString != null)
          Msg = resourceString;
      }
      return Msg;
    }

    /// <summary>Returns or sets a <see langword="String" /> expression containing a descriptive string associated with an error. Read/write.</summary>
    /// <returns>Returns or sets a <see langword="String" /> expression containing a descriptive string associated with an error. Read/write.</returns>
    public string Description
    {
      get
      {
        if (this.m_DescriptionIsSet)
          return this.m_curDescription;
        if (this.m_curException == null)
          return "";
        this.Description = this.FilterDefaultMessage(this.m_curException.Message);
        return this.m_curDescription;
      }
      set
      {
        this.m_curDescription = value;
        this.m_DescriptionIsSet = true;
      }
    }

    /// <summary>Returns or sets a <see langword="String" /> expression containing the fully qualified path to a Help file. Read/write.</summary>
    /// <returns>Returns or sets a <see langword="String" /> expression containing the fully qualified path to a Help file. Read/write.</returns>
    public string HelpFile
    {
      get
      {
        if (this.m_HelpFileIsSet)
          return this.m_curHelpFile;
        if (this.m_curException == null)
          return "";
        this.ParseHelpLink(this.m_curException.HelpLink);
        return this.m_curHelpFile;
      }
      set
      {
        this.m_curHelpFile = value;
        this.m_HelpFileIsSet = true;
      }
    }

    private string MakeHelpLink(string HelpFile, int HelpContext)
    {
      return HelpFile + "#" + Conversions.ToString(HelpContext);
    }

    private void ParseHelpLink(string HelpLink)
    {
      if (HelpLink == null || HelpLink.Length == 0)
      {
        if (!this.m_HelpContextIsSet)
          this.HelpContext = 0;
        if (this.m_HelpFileIsSet)
          return;
        this.HelpFile = "";
      }
      else
      {
        int length = Strings.m_InvariantCompareInfo.IndexOf(HelpLink, "#", CompareOptions.Ordinal);
        if (length != -1)
        {
          if (!this.m_HelpContextIsSet)
            this.HelpContext = length >= HelpLink.Length ? 0 : Conversions.ToInteger(HelpLink.Substring(checked (length + 1)));
          if (this.m_HelpFileIsSet)
            return;
          this.HelpFile = HelpLink.Substring(0, length);
        }
        else
        {
          if (!this.m_HelpContextIsSet)
            this.HelpContext = 0;
          if (this.m_HelpFileIsSet)
            return;
          this.HelpFile = HelpLink;
        }
      }
    }

    /// <summary>Returns or sets an <see langword="Integer" /> containing the context ID for a topic in a Help file. Read/write.</summary>
    /// <returns>Returns or sets an <see langword="Integer" /> containing the context ID for a topic in a Help file. Read/write.</returns>
    public int HelpContext
    {
      get
      {
        if (this.m_HelpContextIsSet)
          return this.m_curHelpContext;
        if (this.m_curException == null)
          return 0;
        this.ParseHelpLink(this.m_curException.HelpLink);
        return this.m_curHelpContext;
      }
      set
      {
        this.m_curHelpContext = value;
        this.m_HelpContextIsSet = true;
      }
    }

    /// <summary>Returns the exception representing the error that occurred.</summary>
    /// <returns>Returns the exception representing the error that occurred.</returns>
    public Exception GetException()
    {
      return this.m_curException;
    }

    /// <summary>Clears all property settings of the <see langword="Err" /> object.</summary>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public void Clear()
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        this.m_curException = (Exception) null;
        this.m_curNumber = 0;
        this.m_curSource = "";
        this.m_curDescription = "";
        this.m_curHelpFile = "";
        this.m_curHelpContext = 0;
        this.m_curErl = 0;
        this.m_NumberIsSet = false;
        this.m_SourceIsSet = false;
        this.m_DescriptionIsSet = false;
        this.m_HelpFileIsSet = false;
        this.m_HelpContextIsSet = false;
        this.m_ClearOnCapture = true;
      }
    }
/*
    /// <summary>Generates a run-time error; can be used instead of the <see langword="Error" /> statement.</summary>
    /// <param name="Number">Required. <see langword="Long" /> integer that identifies the nature of the error. Visual Basic errors are in the range 0–65535; the range 0–512 is reserved for system errors; the range 513–65535 is available for user-defined errors. When setting the <see langword="Number" /> property to your own error code in a class module, you add your error code number to the <see langword="vbObjectError" /> constant. For example, to generate the error number 513, assign vbObjectError + 513 to the <see langword="Number" /> property.</param>
    /// <param name="Source">Optional. <see langword="String" /> expression naming the object or application that generated the error. When setting this property for an object, use the form <paramref name="project" />.<paramref name="class" />. If <paramref name="Source" /> is not specified, the process ID of the current Visual Basic project is used.</param>
    /// <param name="Description">Optional. <see langword="String" /> expression describing the error. If unspecified, the value in the <see langword="Number" /> property is examined. If it can be mapped to a Visual Basic run-time error code, the string that would be returned by the <see langword="Error" /> function is used as the <see langword="Description" /> property. If there is no Visual Basic error corresponding to the <see langword="Number" /> property, the "Application-defined or object-defined error" message is used.</param>
    /// <param name="HelpFile">Optional. The fully qualified path to the Help file in which help on this error can be found. If unspecified, Visual Basic uses the fully qualified drive, path, and file name of the Visual Basic Help file.</param>
    /// <param name="HelpContext">Optional. The context ID identifying a topic within <paramref name="HelpFile" /> that provides help for the error. If omitted, the Visual Basic Help-file context ID for the error corresponding to the <see langword="Number" /> property is used, if it exists.</param>
    public void Raise(int Number, object Source = null, object Description = null, object HelpFile = null, object HelpContext = null)
    {
      if (Number == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Number)
        }));
      this.Number = Number;
      if (Source != null)
      {
        this.Source = Conversions.ToString(Source);
      }
      else
      {
        IVbHost vbHost = HostServices.VBHost;
        if (vbHost == null)
        {
          string fullName = Assembly.GetCallingAssembly().FullName;
          int num = Strings.InStr(fullName, ",", CompareMethod.Binary);
          this.Source = num >= 1 ? Strings.Left(fullName, checked (num - 1)) : fullName;
        }
        else
          this.Source = vbHost.GetWindowTitle();
      }
      if (HelpFile != null)
        this.HelpFile = Conversions.ToString(HelpFile);
      if (HelpContext != null)
        this.HelpContext = Conversions.ToInteger(HelpContext);
      if (Description != null)
        this.Description = Conversions.ToString(Description);
      else if (!this.m_DescriptionIsSet)
        this.Description = Utils.GetResourceString((vbErrors) this.m_curNumber);
      Exception exception = this.MapNumberToException(this.m_curNumber, this.m_curDescription);
      exception.Source = this.m_curSource;
      exception.HelpLink = this.MakeHelpLink(this.m_curHelpFile, this.m_curHelpContext);
      this.m_ClearOnCapture = false;
      throw exception;
    }
*/
    /// <summary>Returns a system error code produced by a call to a dynamic-link library (DLL). Read-only.</summary>
    /// <returns>Returns a system error code produced by a call to a dynamic-link library (DLL). Read-only.</returns>
    public int LastDllError
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return Marshal.GetLastWin32Error();
      }
    }

    internal void SetUnmappedError(int Number)
    {
      this.Clear();
      this.Number = Number;
      this.m_ClearOnCapture = false;
    }

    internal Exception CreateException(int Number, string Description)
    {
      this.Clear();
      this.Number = Number;
      if (Number == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Number)
        }));
      Exception exception = this.MapNumberToException(this.m_curNumber, Description);
      this.m_ClearOnCapture = false;
      return exception;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal void CaptureException(Exception ex)
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        if (ex != this.m_curException)
        {
          if (this.m_ClearOnCapture)
            this.Clear();
          else
            this.m_ClearOnCapture = true;
          this.m_curException = ex;
        }
      }
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal void CaptureException(Exception ex, int lErl)
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        this.CaptureException(ex);
        this.m_curErl = lErl;
      }
    }

    private int MapExceptionToNumber(Exception e)
    {
      Type type = e.GetType();
      if (type == typeof (IndexOutOfRangeException) || type == typeof (RankException))
        return 9;
      if (type == typeof (DivideByZeroException))
        return 11;
      if (type == typeof (OverflowException))
        return 6;
      if (type == typeof (NotFiniteNumberException))
        return ((NotFiniteNumberException) e).OffendingNumber == 0.0 ? 11 : 6;
      if (type == typeof (NullReferenceException))
        return 91;
      if (e is AccessViolationException)
        return -2147467261;
      if (type == typeof (InvalidCastException) || type == typeof (NotSupportedException))
        return 13;
      if (type == typeof (COMException))
        return Utils.MapHRESULT(((ExternalException) e).ErrorCode);
      if (type == typeof (SEHException))
        return 99;
      if (type == typeof (DllNotFoundException))
        return 53;
      if (type == typeof (EntryPointNotFoundException))
        return 453;
      if (type == typeof (TypeLoadException))
        return 429;
      if (type == typeof (OutOfMemoryException))
        return 7;
      if (type == typeof (FormatException))
        return 13;
      if (type == typeof (DirectoryNotFoundException))
        return 76;
      if (type == typeof (IOException))
        return 57;
      if (type == typeof (FileNotFoundException))
        return 53;
      if (e is MissingMemberException)
        return 438;
      return e is InvalidOleVariantTypeException ? 458 : 5;
    }

    private Exception MapNumberToException(int Number, string Description)
    {
      int Number1 = Number;
      string Description1 = Description;
      bool flag = false;
      ref bool local = ref flag;
      return ExceptionUtils.BuildException(Number1, Description1, ref local);
    }

    internal int MapErrorNumber(int Number)
    {
      if (Number > (int) ushort.MaxValue)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Number)
        }));
      if (Number >= 0)
        return Number;
      if ((Number & 536805376) == 655360)
        return Number & (int) ushort.MaxValue;
      switch (Number)
      {
        case -2147467263:
          return 445;
        case -2147467262:
          return 430;
        case -2147467261:
          return -2147467261;
        case -2147467260:
          return 287;
        case -2147352575:
          return 438;
        case -2147352573:
          return 438;
        case -2147352572:
          return 448;
        case -2147352571:
          return 13;
        case -2147352570:
          return 438;
        case -2147352569:
          return 446;
        case -2147352568:
          return 458;
        case -2147352566:
          return 6;
        case -2147352565:
          return 9;
        case -2147352564:
          return 447;
        case -2147352563:
          return 10;
        case -2147352562:
          return 450;
        case -2147352561:
          return 449;
        case -2147352559:
          return 451;
        case -2147352558:
          return 11;
        case -2147319786:
          return 32790;
        case -2147319785:
          return 461;
        case -2147319784:
          return 32792;
        case -2147319783:
          return 32793;
        case -2147319780:
          return 32796;
        case -2147319779:
          return 32797;
        case -2147319769:
          return 32807;
        case -2147319768:
          return 32808;
        case -2147319767:
          return 32809;
        case -2147319766:
          return 32810;
        case -2147319765:
          return 32811;
        case -2147319764:
          return 32812;
        case -2147319763:
          return 32813;
        case -2147319762:
          return 32814;
        case -2147319761:
          return 453;
        case -2147317571:
          return 35005;
        case -2147317563:
          return 35013;
        case -2147316576:
          return 13;
        case -2147316575:
          return 9;
        case -2147316574:
          return 57;
        case -2147316573:
          return 322;
        case -2147312566:
          return 48;
        case -2147312509:
          return 40067;
        case -2147312508:
          return 40068;
        case -2147287039:
          return 32774;
        case -2147287038:
          return 53;
        case -2147287037:
          return 76;
        case -2147287036:
          return 67;
        case -2147287035:
          return 70;
        case -2147287034:
          return 32772;
        case -2147287032:
          return 7;
        case -2147287022:
          return 67;
        case -2147287021:
          return 70;
        case -2147287015:
          return 32771;
        case -2147287011:
          return 32773;
        case -2147287010:
          return 32772;
        case -2147287008:
          return 75;
        case -2147287007:
          return 70;
        case -2147286960:
          return 58;
        case -2147286928:
          return 61;
        case -2147286789:
          return 32792;
        case -2147286788:
          return 53;
        case -2147286787:
          return 32792;
        case -2147286786:
          return 32768;
        case -2147286784:
          return 70;
        case -2147286783:
          return 70;
        case -2147286782:
          return 32773;
        case -2147286781:
          return 57;
        case -2147286780:
          return 32793;
        case -2147286779:
          return 32793;
        case -2147286778:
          return 32789;
        case -2147286777:
          return 32793;
        case -2147286776:
          return 32793;
        case -2147221230:
          return 429;
        case -2147221164:
          return 429;
        case -2147221021:
          return 429;
        case -2147221018:
          return 432;
        case -2147221014:
          return 432;
        case -2147221005:
          return 429;
        case -2147221003:
          return 429;
        case -2147220994:
          return 429;
        case -2147024891:
          return 70;
        case -2147024882:
          return 7;
        case -2147024809:
          return 5;
        case -2147023174:
          return 462;
        case -2146959355:
          return 429;
        default:
          return Number;
      }
    }
  }
}
